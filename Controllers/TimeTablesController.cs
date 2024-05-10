using ATLANT.DTO;
using ATLANT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATLANT.Controllers
{
    [Route("api/[controller]")] // добавление к маршруту
    [EnableCors]
    [ApiController]
    public class TimeTablesController : Controller
    {
        private readonly FitnesContext _context;

        public TimeTablesController(FitnesContext context)
        {
            _context = context;
        }

        // GET: TimeTablesController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeTable>>> GetTimeTables()
        {
            return await _context.TimeTable.ToListAsync();
        }

        // GET: TimeTablesController/Details/5
        // Вывести одну запись по нужному id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TimeTable>>> GetTimeTable(int id)
        {
            var timetable = await _context.TimeTable.FindAsync(id);

            if (timetable == null)
            {
                return NotFound();
            }
            return Ok(timetable);
        }

        // POST api/<TimeTablesController>
        // Добавление нового абонемента

        [HttpPost]
        public async Task<ActionResult<TimeTable>> PostShedule(TimeTableDTO newTimeTable)
        {
            var timetable = new TimeTable
            {
                MaxCount = newTimeTable.MaxCount,
                Date = newTimeTable.Date,
                TimeStart = newTimeTable.TimeStart,
                TimeEnd = newTimeTable.TimeEnd,
                CoachId = newTimeTable.CoachId,
                ServiceTypeId = newTimeTable.ServiceTypeId,
                TypeTrainingId = newTimeTable.TypeTrainingId
            };

            _context.TimeTable.Add(timetable);
            await _context.SaveChangesAsync();

            return Ok(timetable);
        }



        // DELETE api/<TimeTablesController>/5
        // Удаление тренировки по id
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Ограничение доступа (Только для админа)
        public async Task<IActionResult> DeleteTimeTables(int id)
        {
            var timetable = await _context.TimeTable.FindAsync(id);
            if (timetable == null)
            {
                return NotFound();
            }
            _context.TimeTable.Remove(timetable);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
