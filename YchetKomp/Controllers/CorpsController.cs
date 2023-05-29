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
    public class CorpsController : ControllerBase
    {
        private readonly user05_2Context _context;

        public CorpsController(user05_2Context context)
        {
            _context = context;
        }

     

   
        // POST: api/Corps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("post")]
        public async Task<ActionResult<Corps>> PostCabinet([FromBody] Corps corps)
        {
            _context.Corps.Add(corps);
            await _context.SaveChangesAsync();
            return corps;
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
