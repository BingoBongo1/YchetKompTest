using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using System.Text.Json;
using YchetKomp.Controllers;
using YchetKomp.DB;
using YchetKomp.Models;

namespace TestYchetKomp.Controllers.Tests
{
    [TestClass()]
    public class DeviceControllerTests
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
        public Device device { get; set; } = new Device
        {
            UserId = 25,
            ManufacturerId = 3,
            DeviceTypeId = 3,
            InventoryNumber = "32124112",
            NameDevice = "wdad",
            Description = "adqw",
            Decommissioned = DateTime.Now,
            CabinetId = 3

        };

        [TestMethod()]
        public async Task GetDevicesTest()
        {
            var controller = new DevicesController(_context);
            var device = _context.Devices.ToList();
            var result = await controller.GetDevices();
            Assert.IsInstanceOfType(result.Value, typeof(IEnumerable<YchetKomp.Models.Device>));
            Assert.AreEqual(device.Count, result.Value.Count());

        }
        [TestMethod()]
        public async Task PutDeviceTest()
        {
            var controller = new DevicesController(_context);
            var device = await _context.Devices.FirstOrDefaultAsync(s => s.Id == 6);
            var result = await controller.PutDevice(device);

            Assert.IsInstanceOfType(result, typeof(OkResult));

        }

        [TestMethod()]
        public async Task DeleteDeviceTest()
        {
            var controller = new DevicesController(_context);
            _context.Devices.Add(device);
            _context.SaveChanges();
            var dev = _context.Devices.ToList();
            var result = await controller.DeleteDevices(dev[dev.Count - 1].Id);
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        //интеграционные тесты 

        [TestMethod()]
        public async Task DeleteDevicesPostIntTest()
        {
            _context.Devices.Add(device);
            _context.SaveChanges();
            var json = JsonSerializer.Serialize(device.Id, device.Id.GetType(), options);
            var response = await client.PostAsync("/api/Devices/delete", new StringContent(json, Encoding.UTF8, "application/json"));
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }


        [TestMethod()]
        public async Task PutDevicesTestIntTest()
        {
            var claim = _context.Devices.FirstOrDefault();
            var response = await client.PostAsync("/api/Devices/put", new StringContent(JsonSerializer.Serialize(claim, options),
            Encoding.UTF8, "application/json"));
            var responseContent = await response.Content.ReadAsStringAsync();
            var returnedDevice = JsonSerializer.Deserialize<Device>(responseContent);
            Assert.IsInstanceOfType(device, returnedDevice.GetType());
        }
    }
}
