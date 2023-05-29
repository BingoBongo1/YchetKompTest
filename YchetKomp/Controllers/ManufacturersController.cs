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
    public class ManufacturersController : ControllerBase
    {
        private readonly user05_2Context _context;

        public ManufacturersController(user05_2Context context)
        {
            _context = context;
        }

        // GET: api/Manufacturers
        [HttpPost("get")]
        public async Task<ActionResult<IEnumerable<Manufacturer>>> GetManufacturers()
        {
            return await _context.Manufacturers.ToListAsync();
        }

       

        // POST: api/Manufacturers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("post")]
        public async Task<ActionResult<Manufacturer>> PostCabinet([FromBody] Manufacturer manufacturer)
        {
            _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync();
            return manufacturer;
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
