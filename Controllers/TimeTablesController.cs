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
        // Добавление новой тренировки

        [HttpPost]
        public async Task<ActionResult<TimeTable>> PostTimeTable(TimeTableDTO newTimeTable)
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
            
            //return timetable;
            return Ok(timetable);
        }

        // PUT api/<TimeTablesController>/5
        // Изменение существующей тренировки
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTimeTable(int id, TimeTableDTO timetable)
        {
            if (id != timetable.Id)
            {
                return BadRequest();
            }

            var itemTimetable = _context.TimeTable.Find(id);
            if (itemTimetable == null)
            {
                return NotFound();
            }

            //проверяем на посещение
            var timetableForVisit = await _context.TimeTable
        .Include(t => t.VisitRegisterTimeTable)
        .FirstOrDefaultAsync(t => t.Id == id);

            if (timetableForVisit == null)
            {
                return NotFound();
            }

            if (timetableForVisit.VisitRegisterTimeTable?.Count > 0)
                {
                // нельзя изменить тренировку с посещениями
                return BadRequest();
            }

            itemTimetable.MaxCount = timetable.MaxCount;
            itemTimetable.Date = timetable.Date;
            itemTimetable.TimeStart = timetable.TimeStart;
            itemTimetable.TimeEnd = timetable.TimeEnd;
            itemTimetable.CoachId = timetable.CoachId;
            itemTimetable.ServiceTypeId = timetable.ServiceTypeId;
            itemTimetable.TypeTrainingId = timetable.TypeTrainingId;

            _context.TimeTable.Update(itemTimetable);
            await _context.SaveChangesAsync();
            return Ok(itemTimetable);
            //return NoContent();
        }


        // DELETE api/<TimeTablesController>/5
        // Удаление тренировки по id
        [HttpDelete("{id}")]
        //[Authorize(Roles = "admin")] // Ограничение доступа (Только для админа)
        public async Task<IActionResult> DeleteTimeTables(int id)
        {
            var timetable = await _context.TimeTable
        .Include(t => t.VisitRegisterTimeTable)
        .FirstOrDefaultAsync(t => t.Id == id);

            if (timetable == null)
            {
                return NotFound();
            }

/*            if (timetable.VisitRegisterTimeTable?.Count > 0)
            {
                return BadRequest("Нельзя удалить тренировку с записанными клиентами.");
            }*/
            _context.TimeTable.Remove(timetable);
            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
