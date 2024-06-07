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

        // Метод для получения русского названия дня недели по английскому дню недели
        private string GetRussianDayOfWeek(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "Понедельник";
                case DayOfWeek.Tuesday:
                    return "Вторник";
                case DayOfWeek.Wednesday:
                    return "Среда";
                case DayOfWeek.Thursday:
                    return "Четверг";
                case DayOfWeek.Friday:
                    return "Пятница";
                case DayOfWeek.Saturday:
                    return "Суббота";
                case DayOfWeek.Sunday:
                    return "Воскресение";
                default:
                    return "Неизвестный день недели";
            }
        }

        // GET: ShedulesController
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shedule>>> GetShedules()
        {
            return await _context.Shedule.Include(g=> g.DayWeek).ToListAsync();
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
        }

        // POST api/<ShedulesController>
        // Добавление новой тренировки шаблона

        [HttpPost]
        public async Task<ActionResult<Shedule>> PostShedule(SheduleDTO newShedule)
        {
            var shedule = new Shedule
            {
                MaxCount = newShedule.MaxCount,
                Date = newShedule.Date,
                TimeStart = newShedule.TimeStart,
                TimeEnd = newShedule.TimeEnd,
                DayWeekId = newShedule.DayWeekId,
                CoachId = newShedule.CoachId,
                ServiceTypeId = newShedule.ServiceTypeId,
                TypeTrainingId = newShedule.TypeTrainingId
            };

            _context.Shedule.Add(shedule);
            await _context.SaveChangesAsync();

            //return shedule;
            return Ok(shedule);
        }

        // POST api/<ShedulesController>
        // Применение шаблона на постоянное расписание

        [HttpPut]
        public async Task<ActionResult> PostSheduleToTimeTable(IEnumerable<DateTime> dates)
        {

            // Проверяем, есть ли уже записи в базе данных по переданным датам
            var existingSchedules = await _context.TimeTable
    .Where(s => dates.Contains(s.Date))
    .ToListAsync();

            if (existingSchedules.Any())
            {
                return BadRequest();
            }

            foreach (var date in dates)
            {
                var dayOfWeek = date.DayOfWeek;
                var russianDayOfWeek = GetRussianDayOfWeek(dayOfWeek);

                var schedulesForDay = _context.Shedule.Where(s => s.DayWeek.Day == russianDayOfWeek).ToList();

                foreach (var schedule in schedulesForDay)
                {
                    var timetable = new TimeTable
                    {
                        MaxCount = schedule.MaxCount,
                        Date = date,
                        TimeStart = schedule.TimeStart,
                        TimeEnd = schedule.TimeEnd,
                        CoachId = schedule.CoachId,
                        ServiceTypeId = schedule.ServiceTypeId,
                        TypeTrainingId = schedule.TypeTrainingId
                    };
                    _context.TimeTable.Add(timetable);
                }
            }
            await _context.SaveChangesAsync();
            return Ok(dates);
        }

// PUT api/<ShedulesController>/5
// Изменение существующей тренировки
[HttpPut("{id}")]
        public async Task<IActionResult> PutShedule(int id, SheduleDTO shedule)
        {
            if (id != shedule.Id)
            {
                return BadRequest();
            }

            var itemShedule = _context.Shedule.Find(id);
            if (itemShedule == null)
            {
                return NotFound();
            }

            itemShedule.MaxCount = shedule.MaxCount;
            itemShedule.Date = shedule.Date;
            itemShedule.TimeStart = shedule.TimeStart;
            itemShedule.TimeEnd = shedule.TimeEnd;
            itemShedule.DayWeekId = shedule.DayWeekId;
            itemShedule.CoachId = shedule.CoachId;
            itemShedule.ServiceTypeId = shedule.ServiceTypeId;
            itemShedule.TypeTrainingId = shedule.TypeTrainingId;

            _context.Shedule.Update(itemShedule);
            await _context.SaveChangesAsync();
            return Ok(itemShedule);
            //return NoContent();
        }


        // DELETE api/<ShedulesController>/5
        // Удаление тренировки по id
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Ограничение доступа (Только для админа)
        public async Task<IActionResult> DeleteShedule(int id)
        {
            var shedule = await _context.Shedule.FindAsync(id);
            if (shedule == null)
            {
                return NotFound();
            }
            _context.Shedule.Remove(shedule);
            await _context.SaveChangesAsync();
            return NoContent();
        }








    }
}
