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
    public class HistoriesController : ControllerBase
    {
        private readonly user05_2Context _context;

        public HistoriesController(user05_2Context context)
        {
            _context = context;
        }

        // GET: api/Histories
        [HttpPost("get")]
        public async Task<ActionResult<IEnumerable<History>>> GetHistories()
        {
            return await _context.Histories.Include(u => u.Device).ToListAsync();
        }




    }
}
