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
    public class DeviceTypesController : ControllerBase
    {
        private readonly user05_2Context _context;

        public DeviceTypesController(user05_2Context context)
        {
            _context = context;
        }

        // GET: api/DeviceTypes
        [HttpPost("get")]
        public async Task<ActionResult<IEnumerable<DeviceType>>> GetDeviceTypes()
        {
            return await _context.DeviceTypes.ToListAsync();
        }

       

        

        // POST: api/DeviceTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("post")]
        public async Task<ActionResult<DeviceType>> PostCabinet([FromBody] DeviceType devicetype)
        {
            _context.DeviceTypes.Add(devicetype);
            await _context.SaveChangesAsync();
            return devicetype;
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
