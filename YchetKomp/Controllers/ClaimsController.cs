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
    public class ClaimsController : ControllerBase
    {
        private readonly user05_2Context _context;

        public ClaimsController(user05_2Context context)
        {
            _context = context;
        }

        // GET: api/Claims
        [HttpPost("getall")]
        public async Task<ActionResult<IEnumerable<Claim>>> GetClaims()
        {           
            return await _context.Claims.Include(s => s.Device).Include(s => s.UserIdOpenNavigation).Include(s => s.UserIdCloseNavigation)
              .Include(s => s.StatusClaim).Include(s => s.Device.Manufacturer).Include(s=>s.Device.Cabinet).Include(s=>s.Device.DeviceType)
             .Include(s=>s.Device.Cabinet.Corps).Include(s => s.Device.User.Role).ToListAsync();
          
        }
              
        // PUT: api/Claims/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("put")]
        public async Task<IActionResult> PutClaim([FromBody] Claim claim)
        {
            _context.Entry(claim).State = EntityState.Modified;
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

        // POST: api/Claims
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("post")]
        public async Task<ActionResult<Claim>> PostClaim([FromBody]Claim claim)
        {
            claim.UserIdOpenNavigation = _context.Users.Find(claim.UserIdOpenNavigation.UserId);
            claim.UserIdCloseNavigation = _context.Users.Find(claim.UserIdOpenNavigation.UserId);
            claim.Device = _context.Devices.Find(claim.Device.Id);
            claim.StatusClaim = _context.StatusClaims.Find(claim.StatusClaim.StatusClaimId);

            claim.UserIdOpenNavigation.Role = _context.Roles.Find(claim.UserIdOpenNavigation.RoleId);
            claim.UserIdCloseNavigation.Role = _context.Roles.Find(claim.UserIdCloseNavigation.RoleId);

            claim.Device.DeviceType = _context.DeviceTypes.Find(claim.Device.DeviceTypeId);
            claim.Device.Manufacturer = _context.Manufacturers.Find(claim.Device.ManufacturerId);
            claim.Device.User = _context.Users.Find(claim.Device.UserId);

            claim.Device.Cabinet = _context.Cabinets.Find(claim.Device.CabinetId);
            claim.Device.User.Role = _context.Roles.Find(claim.Device.User.RoleId);
            claim.Device.Cabinet.Corps = _context.Corps.Find(claim.Device.Cabinet.CorpsId);
             

            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();

            return claim;
        }

        // DELETE: api/Claims/5
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteClaims([FromBody] int id)
        {
            var claim = await _context.Claims.FindAsync(id);
           
            _context.Claims.Remove(claim);

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
