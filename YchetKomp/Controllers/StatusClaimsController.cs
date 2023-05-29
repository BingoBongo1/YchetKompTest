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
    public class StatusClaimsController : ControllerBase
    {
        private readonly user05_2Context _context;

        public StatusClaimsController(user05_2Context context)
        {
            _context = context;
        }

        // GET: api/StatusClaims
        [HttpPost("get")]
        public async Task<ActionResult<IEnumerable<StatusClaim>>> GetStatusClaims()
        {
            return await _context.StatusClaims.ToListAsync();
        }

        // GET: api/StatusClaims/5
             

        // POST: api/StatusClaims
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("post")]
        public async Task<ActionResult<StatusClaim>> PostStatusClaim([FromBody] StatusClaim statusClaim)
        {
            _context.StatusClaims.Add(statusClaim);
            await _context.SaveChangesAsync();
            return statusClaim;
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
