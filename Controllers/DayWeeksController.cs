using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ATLANT.Models;
using ATLANT.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace ATLANT.Controllers
{
    [Route("api/[controller]")] // добавление к маршруту. То есть можем указать просто DayWeeks
    [EnableCors]
    [ApiController]
    public class DayWeeksController : Controller
    {
        private readonly FitnesContext _context;

        public DayWeeksController(FitnesContext context)
        {
            _context = context;
        }

        // GET: DayWeeksController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DayWeek>>> GetDayWeeks()
        {
            return await _context.DayWeek.ToListAsync();
        }

        // GET: DayWeeksController/Details/5
        // Вывести одну запись по нужному id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<DayWeek>>> GetDayWeek(int id)
        {
            var dayweek = await _context.DayWeek.FindAsync(id);

            if (dayweek == null)
            {
                return NotFound();
            }
            return Ok(dayweek);
            //return abonement;
        }
    }
}
