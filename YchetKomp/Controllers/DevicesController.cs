using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YchetKomp.DB;
using YchetKomp.Models;

namespace YchetKomp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly user05_2Context _context;

        public DevicesController(user05_2Context context)
        {
            _context = context;
        }      

        [HttpPost("get")]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {

            return await _context.Devices.Include(s=> s.User.Role).Include(s=>s.Cabinet.Corps).Include(s=>s.Cabinet.Devices)
                .Include(s=> s.Manufacturer).Include(s => s.User).Include(s => s.Cabinet).Include(s=> s.DeviceType).ToListAsync();
        }

        // PUT: api/Devices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("put")]
        public async Task<IActionResult> PutDevice([FromBody] Device device)
        {
            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost("post")]
        public async Task<ActionResult<Device>> PostDevice([FromBody]Device device)
        {

            device.Cabinet = _context.Cabinets.Find(device.Cabinet.CabinetId);
            device.DeviceType = _context.DeviceTypes.Find(device.DeviceType.DeviceTypeId);
            device.Manufacturer = _context.Manufacturers.Find(device.Manufacturer.ManufacturerId);
            device.User = _context.Users.Find(device.User.UserId);
            device.Cabinet.Corps = _context.Corps.Find(device.Cabinet.CorpsId);
            device.User.Role = _context.Roles.Find(device.User.RoleId);

            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return device;
        }
        // DELETE: api/Devices/5
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteDevices([FromBody]int id)
        {
            var device = await _context.Devices.FindAsync(id);
            _context.Devices.Remove(device);

            try
            {
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
