using ATLANT.DTO;
using ATLANT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Xml;

namespace ATLANT.Controllers
{
    [Route("api/[controller]")] // добавление к маршруту.
    [EnableCors]
    [ApiController]
    public class VisitRegistersController : Controller
    {
        private readonly FitnesContext _context;

        public VisitRegistersController(FitnesContext context)
        {
            _context = context;
        }

        // Получаем все записи
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitRegister>>> GetVisitRegisters()
        {
            var today = DateTime.Today;
            var allVisits = await _context.VisitRegister.ToListAsync();
            var allTimetables = await _context.TimeTable.ToListAsync();
            var visitRegisters = allVisits.Where(vr => allTimetables.Any(tt => tt.Id == vr.TimeTableId && tt.Date < today));

            foreach (var visitRegister in visitRegisters)
            {
                visitRegister.VisitDate = true;
            }

            await _context.SaveChangesAsync();

            return await _context.VisitRegister.ToListAsync();
        }

        // Получаем одну запись
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<VisitRegister>>> GetVisitRegister(int id)
        {
            var visit = await _context.VisitRegister.FindAsync(id);

            if (visit == null)
            {
                return NotFound();
            }
            return Ok(visit);
        }

        // GET api/<VisitRegistersController>/5
        // Вывести все записи клиента по абонементу
        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<VisitRegister>>> GetVisitRegistersByClient(int clientId)
        {
            // сначала проверяем на дату все записи
            var today = DateTime.Today;
            var allVisits = await _context.VisitRegister.ToListAsync();
            var allTimetables = await _context.TimeTable.ToListAsync();
            var visitRegisters = allVisits.Where(vr => allTimetables.Any(tt => tt.Id == vr.TimeTableId && tt.Date < today));

            foreach (var visitRegister in visitRegisters)
            {
                visitRegister.VisitDate = true;
            }

            await _context.SaveChangesAsync();
            /////

            var visits = await _context.VisitRegister.Include(p => p.Payment).Where(p => p.Payment.UserId == clientId).ToListAsync();

             if (visits == null || !visits.Any())
            {
                 return NotFound();
             }

            return Ok(visits);
        }

        // POST api/<VisitRegistersController>
        // Добавление новой записи на тренировку
        [HttpPost]
        public async Task<ActionResult<VisitRegisterDTO>> PostVisitRegister([FromBody] VisitRegisterDTO visitRegister)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Получение клиента по идентификатору
            var payment = await _context.Payment.FindAsync(visitRegister.PaymentId);
            if (payment == null)
            {
                return NotFound($"Оплата с данным id {visitRegister.PaymentId} не найдена.");
            }
            var timetable = await _context.TimeTable.FindAsync(visitRegister.TimeTableId);
            if (timetable == null)
            {
                return NotFound($"Тренировка с данным id {visitRegister.TimeTableId} не найдена.");
            }

            // Проверка наличия существующей записи
            var existingVisitRegister = await _context.VisitRegister
                .FirstOrDefaultAsync(vr => vr.PaymentId == visitRegister.PaymentId && vr.TimeTableId == visitRegister.TimeTableId);

            if (existingVisitRegister != null)
            {
                return Conflict("Запись на тренировку для данной пары (оплата, тренировка) уже существует.");
            }

            payment.CountRemainTraining -= 1;
            _context.Payment.Update(payment);

            VisitRegister visit = new VisitRegister
            {
                IsPresent = visitRegister.IsPresent,
                VisitDate = visitRegister.VisitDate,
                TimeTableId = visitRegister.TimeTableId,
                PaymentId = visitRegister.PaymentId
            };

            _context.VisitRegister.Add(visit);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<VisitRegistersController>/5
        // Изменение существующей записи - Отмечаем посещаемость
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVisitRegister(int id, VisitRegisterDTO visit)
        {
            if (id != visit.Id)
            {
                return BadRequest();
            }

            var visitReg = _context.VisitRegister.Find(id);
            if (visitReg == null)
            {
                return NotFound();
            }

            visitReg.IsPresent = visit.IsPresent;
            _context.VisitRegister.Update(visitReg);
            await _context.SaveChangesAsync();
            return Ok(visitReg);
        }

        // DELETE api/<VisitRegistersController>/5
        // Удаление записи по id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitRegister(int id)
        {
            var visit = await _context.VisitRegister.FindAsync(id);
            if (visit == null)
            {
                return NotFound();
            }
            // При отмене действующей записи
            var payment = await _context.Payment.FindAsync(visit.PaymentId);
            payment.CountRemainTraining += 1;
            _context.Payment.Update(payment);
            _context.VisitRegister.Remove(visit);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
