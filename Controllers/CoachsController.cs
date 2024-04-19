using ATLANT.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATLANT.Controllers
{
    [Route("api/[controller]")] // добавление к маршруту. То есть можем указать просто Coachs
    [EnableCors]
    [ApiController]
    public class CoachsController : Controller
    {
        private readonly FitnesContext _context;

        public CoachsController(FitnesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coach>>> GetCoachs()
        {
            return await _context.Coachs.Include(g => g.Shedule).Include(g => g.User).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Coach>>> GetCoach(int id)
        {
            var coach = await _context.Coachs.FindAsync(id);

            if (coach == null)
            {
                return NotFound();
            }
            return Ok(coach);
        }
    }
}
