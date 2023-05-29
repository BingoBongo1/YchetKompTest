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
    public class CabinetsController : ControllerBase
    {
        private readonly user05_2Context _context;

        public CabinetsController(user05_2Context context)
        {
            _context = context;
        }

        // GET: api/Cabinets
        [HttpPost("get")]
        public async Task<ActionResult<IEnumerable<Cabinet>>> GetCabinets()
        {

            return await _context.Cabinets.Include(s => s.Corps).Include(s => s.Devices).ToListAsync();
        }

      
       

        // POST: api/Cabinets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("post")]
        public async Task<ActionResult<Cabinet>> PostCabinet([FromBody]Cabinet cabinet)
        {
            cabinet.Corps = _context.Corps.Find(cabinet.Corps.CorpsId);
            _context.Cabinets.Add(cabinet);
            await _context.SaveChangesAsync();
            return cabinet;
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
