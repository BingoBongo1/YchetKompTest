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
    public class UsersController : ControllerBase
    {
        private readonly user05_2Context _context;

        public UsersController(user05_2Context context)
        {
            _context = context;
        }

        [HttpPost("get")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersPost()
        {
            return await _context.Users.Include(u => u.Role).ToListAsync();
        }

       

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("put")]
        public async Task<IActionResult> PutUser([FromBody] User user)
        {
            var origin = await _context.Users.FindAsync(user.UserId);

            _context.Entry(origin).CurrentValues.SetValues(user);
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("SignUp")]
        public async Task<ActionResult<User>> PostUser([FromBody]User user)
        {
            user.Role = _context.Roles.Find(user.RoleId);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        [HttpPost("Auth")]
        public async Task<ActionResult<User>> AuthUser([FromBody]Auth auth)
        {
            var user = await _context.Users.FirstOrDefaultAsync(s => s.UserLogin == auth.Login && s.UserPassword == auth.Password);

            return user ?? new User();
        }

        // DELETE: api/Users/5
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteUsers([FromBody]int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

      
    }
}
