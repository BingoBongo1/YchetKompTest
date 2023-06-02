using Microsoft.VisualStudio.TestTools.UnitTesting;
using YchetKomp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YchetKomp.Models;
using YchetKomp.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;

namespace TestYchetKomp.Controllers.Tests
{
    [TestClass()]
    public class UsersControllerTests
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
        public User user { get; set; } = new User
        {
            UserPassword = "1",
            UserLogin = "1",
            UserSurname = "1",
            UserName = "1",
            RoleId = 3
        };
        public Auth auth { get; set; } = new Auth { Login = "admin", Password = "admin" };
        [TestMethod()]
        public async Task GetUsersPostTest()
        {
            var controller = new UsersController(_context);
            var users = new List<User>();
            users = _context.Users.ToList();
            var result = await controller.GetUsersPost();
            Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<User>));
            Assert.AreEqual(users.Count, result.Value.Count());
        }

        [TestMethod()]
        public async Task PutUserTest()
        {
            var controller = new UsersController(_context);
            var userdel = await _context.Users.FirstOrDefaultAsync(s => s.UserId == 25);
            var result = await controller.PutUser(userdel);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod()]
        public async Task PostUserTest()
        {
            var controller = new UsersController(_context);
            var result = await controller.PostUser(user);
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.ActionResult<YchetKomp.Models.User>));
            _context.Users.Remove(user);
        }

        [TestMethod()]
        public async Task AuthUserTest()
        {
            var controller = new UsersController(_context);
            var result = await controller.AuthUser(auth);
            Assert.IsInstanceOfType(result, typeof(Microsoft.AspNetCore.Mvc.ActionResult<YchetKomp.Models.User>));
        }

        [TestMethod()]
        public async Task DeleteUsersTest()
        {
            var controller = new UsersController(_context);
            _context.Users.Add(user);
            _context.SaveChanges();
            var users = _context.Users.ToList();
            var result = await controller.DeleteUsers(users[users.Count - 1].UserId);
            Assert.IsInstanceOfType(result, typeof(OkResult));

        }

        //интеграционные тесты
        [TestMethod()]
        public async Task GetUsersPostIntTest()
        {
            var response = await client.PostAsync("/api/Users/get", null);
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod()]
        public async Task DeleteUsersPost_IntTest()
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            var json = JsonSerializer.Serialize(user.UserId, user.UserId.GetType(), options);
            var response = await client.PostAsync("/api/Users/delete", new StringContent(json, Encoding.UTF8, "application/json"));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod()]
        public async Task AuthUserTest_IntTest()
        {
            var response = await client.PostAsync("/api/Users/Auth", new StringContent(JsonSerializer.Serialize(auth, options),
                Encoding.UTF8, "application/json"));
            var responseContent = await response.Content.ReadAsStringAsync();
            var returnedUser = JsonSerializer.Deserialize<User>(responseContent);
            Assert.IsInstanceOfType(user, returnedUser.GetType());
        }
        [TestMethod()]
        public async Task PostUserSignUpTest_IntTest()
        {
            var response = await client.PostAsync("/api/Users/SignUp", new StringContent(JsonSerializer.Serialize(user, options),
                Encoding.UTF8, "application/json"));
            var responseContent = await response.Content.ReadAsStringAsync();
            var returnedUser = JsonSerializer.Deserialize<User>(responseContent);
            Assert.IsInstanceOfType(user, returnedUser.GetType());
        }
        [TestMethod()]
        public async Task PutUserSignUpTest_IntTest()
        {
            var user = _context.Users.FirstOrDefault();
            var response = await client.PostAsync("/api/Users/put", new StringContent(JsonSerializer.Serialize(user, options),
                Encoding.UTF8, "application/json"));
            var responseContent = await response.Content.ReadAsStringAsync();
            var returnedUser = JsonSerializer.Deserialize<User>(responseContent);
            Assert.IsInstanceOfType(user, returnedUser.GetType());
        }
    }
}
