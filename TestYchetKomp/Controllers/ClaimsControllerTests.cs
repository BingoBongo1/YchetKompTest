using Microsoft.VisualStudio.TestTools.UnitTesting;
using YchetKomp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YchetKomp.DB;
using YchetKomp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using System.Net;

namespace TestYchetKomp.Controllers.Tests
{
    [TestClass()]
    public class ClaimsControllerTests
    {
        [TestInitialize]
        public void Initialize()
        {
            _factory = new WebApplicationFactory<Program>();
            client = _factory.CreateClient();
        }
        private user05_2Context _context = new user05_2Context();
        private WebApplicationFactory<Program> _factory;
        private HttpClient client;
        static JsonSerializerOptions options = new JsonSerializerOptions 
        {
            PropertyNameCaseInsensitive = true,
            ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
        };
        public Claim claim { get; set; } = new Claim 
        {
            StatusClaimId = 1,
            DeviceId = 6,
            DateOpen = DateTime.Now,
            DateClose = DateTime.Now,
            Description = "проверка пс",
            UserIdClose = 25,
            UserIdOpen = 25
        };
        [TestMethod()]
        public async Task GetClaimsTest() 
        {
            var controller = new ClaimsController(_context);          
            var claims = _context.Claims.ToList();
            var result = await controller.GetClaims();
            Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<Claim>));
            Assert.AreEqual(claims.Count, result.Value.Count());
        }

        [TestMethod()]
        public async Task PutClaimTest()
        {
            var controller = new ClaimsController(_context);
            var claimdel = await _context.Claims.FirstOrDefaultAsync(s => s.ClaimsId == 2); 
            var result = await controller.PutClaim(claimdel);                                                    
            Assert.IsInstanceOfType(result, typeof(OkResult)); 
        }

        [TestMethod()]
        public async Task PostClaimTest()
        {
            var controller = new ClaimsController(_context);
            var result = await controller.PostClaim(claim);
            Assert.IsInstanceOfType(result, typeof(ActionResult<Claim>)); 
            _context.Claims.Remove(result.Value);
        }

        [TestMethod()]
        public async Task DeleteClaimsTest()
        {
            var controller = new ClaimsController(_context);
            _context.Claims.Add(claim);
            _context.SaveChanges();
            var claims = _context.Claims.ToList(); 
            var result = await controller.DeleteClaims(claims[claims.Count - 1].ClaimsId);           
            Assert.IsInstanceOfType(result, typeof(OkResult));
           
        }

        //интеграционные тесты

        [TestMethod()]
        public async Task GetClaimsPostIntTest()
        {
            var response = await client.PostAsync("/api/Claims/getall", null);
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod()]
        public async Task DeleteClaimsPostIntTest()
        {
            _context.Claims.Add(claim);
            _context.SaveChanges();
            var json = JsonSerializer.Serialize(claim.ClaimsId, claim.ClaimsId.GetType(), options); 
            var response = await client.PostAsync("/api/Claims/delete", new StringContent(json, Encoding.UTF8, "application/json"));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod()]
        public async Task PostClaimsTestIntTest() 
        { 
            var response = await client.PostAsync("/api/Claims/post", new StringContent(JsonSerializer.Serialize(claim, options),
                Encoding.UTF8, "application/json"));
            var responseContent = await response.Content.ReadAsStringAsync();
            var returnedClaim = JsonSerializer.Deserialize<Claim>(responseContent);
                                
            Assert.IsInstanceOfType(claim, returnedClaim.GetType());
        }
        [TestMethod()]
        public async Task PutClaimsTestIntTest()
        {
            var claim = _context.Claims.FirstOrDefault();
            var response = await client.PostAsync("/api/Claims/put", new StringContent(JsonSerializer.Serialize(claim, options),
                Encoding.UTF8, "application/json"));
            var responseContent = await response.Content.ReadAsStringAsync();
            var returnedClaim = JsonSerializer.Deserialize<Claim>(responseContent);         
            Assert.IsInstanceOfType(claim, returnedClaim.GetType());
        }
    }
}
