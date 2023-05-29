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


namespace TestYchetKomp.Controllers.Tests
{
    [TestClass()]
    public class ClaimsControllerTests
    {
        private user05_2Context _context = new user05_2Context(); 
        public Claim claim { get; set; } = new Claim 
        {
            StatusClaimId = 1,
            DeviceId = 6,
            DateOpen = DateTime.Now,
            DateClose = DateTime.Now,
            Description = "123",
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
    }
}
