using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ATLANT.Models;
using ATLANT.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace ATLANT.Controllers
{
    [Route("api/[controller]")] // добавление к маршруту. То есть можем указать просто Shedules
    [EnableCors]
    [ApiController]
    public class ShedulesController : Controller
    {
        private readonly FitnesContext _context;

        public ShedulesController(FitnesContext context)
        {
            _context = context;
        }

        // GET: ShedulesController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shedule>>> GetShedules()
        {
            return await _context.Shedule.Include(g=> g.DayWeek).Include(g => g.Coach.User).Include(g => g.TypeTraining).Include(g => g.ServiceType).ToListAsync();
        }

        // GET: ShedulesController/Details/5
        // Вывести одну запись по нужному id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Shedule>>> GetShedule(int id)
        {
            var shedule = await _context.Shedule.FindAsync(id);

            if (shedule == null)
            {
                return NotFound();
            }
            return Ok(shedule);
            //return abonement;
        }

        // POST api/<ShedulesController>
        // Добавление нового абонемента
        

        // POST: ShedulesController/Create
        

       
       
        

       

    }
}
