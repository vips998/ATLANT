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
            var visitRegisters = await _context.VisitRegister
                 .Include(vr => vr.PaymentVisits)
        .Include(vr => vr.VisitRegisterTimeTables)
        .ThenInclude(vrt => vrt.TimeTable)
        .Where(vr => vr.VisitRegisterTimeTables.Any(vrt => vrt.TimeTable.Date < today))
        .ToListAsync();

            foreach (var visitRegister in visitRegisters)
            {
                visitRegister.VisitDate = true;
            }

            await _context.SaveChangesAsync();

            return await _context.VisitRegister.Include(vr => vr.PaymentVisits)
        .Include(vr => vr.VisitRegisterTimeTables).ToListAsync();
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
            var visitRegisters  = await _context.VisitRegister
        .Include(vr => vr.PaymentVisits)
            .ThenInclude(pv => pv.Payment)
                .ThenInclude(p => p.PaymentAbonement)
                .ThenInclude(p => p.Abonement)
        .Include(vr => vr.VisitRegisterTimeTables)
            .ThenInclude(vrt => vrt.TimeTable)
        .Where(vr => vr.PaymentVisits.Any(pv => pv.Payment.UserId == clientId) &&
                     vr.VisitRegisterTimeTables.Any(vrt => vrt.TimeTable.Date < today))
        .ToListAsync();

            foreach (var visitRegister in visitRegisters)
            {
                visitRegister.VisitDate = true;
            }

            await _context.SaveChangesAsync();
            /////
            var visits = await _context.VisitRegister.Include(vs => vs.PaymentVisits).ThenInclude(pv => pv.Payment)
                .ThenInclude(p => p.PaymentAbonement)
                .ThenInclude(p => p.Abonement)
        .Include(vs => vs.VisitRegisterTimeTables)
    .Where(vr => vr.PaymentVisits.Any(pv => pv.Payment.UserId == clientId)).ToListAsync();

             if (visits == null || !visits.Any())
            {
                 return NotFound();
             }

            return Ok(visits);
        }

        // POST api/<VisitRegistersController>
        // Добавление новой записи на тренировку
        [HttpPost]
        public async Task<ActionResult<VisitRegisterDTO>> PostVisitRegister(VisitRegisterDTO visitRegister)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Получение клиента по идентификатору
            var payment = await _context.Payment.FindAsync(visitRegister.paymentId);
            if (payment == null)
            {
                return NotFound($"Оплата с данным id {visitRegister.paymentId} не найдена.");
            }
            var timetable = await _context.TimeTable.FindAsync(visitRegister.timetableId);
            if (timetable == null)
            {
                return NotFound($"Тренировка с данным id {visitRegister.timetableId} не найдена.");
            }
            
            var visitTimeTable = await _context.VisitRegisterTimeTable.Where(vr => vr.TimeTableId == visitRegister.timetableId).ToListAsync();
            var payVisit = await _context.PaymentVisit.Where(vr => vr.PaymentId == visitRegister.paymentId).ToListAsync();




            // Проверка наличия существующей записи
            var existingVisitRegister = visitTimeTable
                    .Join(payVisit, vtt => vtt.TimeTableId, pv => pv.VisitRegisterId, (vtt, pv) => vtt)
                    .FirstOrDefault();

            if (existingVisitRegister != null)
            {
                return Conflict("Запись на тренировку для данной пары (оплата, тренировка) уже существует.");
            }

            payment.CountRemainTraining -= 1;
            _context.Payment.Update(payment);

            // Создание объектов
            VisitRegister visit = new VisitRegister
            {
                IsPresent = visitRegister.IsPresent,
                VisitDate = visitRegister.VisitDate,
                PaymentVisits = new List<PaymentVisit>(),
                VisitRegisterTimeTables = new List<VisitRegisterTimeTable>(),
            };

            VisitRegisterTimeTable visitTime = new VisitRegisterTimeTable
            {
                // TimeTableId будет присвоен после сохранения VisitRegister, так как VisitRegisterId еще не существует
                TimeTableId = visitRegister.timetableId,
            };

            PaymentVisit paymentVisit = new PaymentVisit
            {
                // PaymentId будет присвоен после сохранения VisitRegister, так как VisitRegisterId еще не существует
                PaymentId = visitRegister.paymentId,
            };

            // Добавление объектов в коллекции
            visit.VisitRegisterTimeTables.Add(visitTime);
            visit.PaymentVisits.Add(paymentVisit);

            // Добавление в контекст и сохранение
            _context.VisitRegister.Add(visit);
            await _context.SaveChangesAsync();

            // Теперь можно присвоить ID, так как visit уже сохранен и имеет ID
            visitTime.VisitRegisterId = visit.Id;
            paymentVisit.VisitRegisterId = visit.Id;

            // Сохраняем изменения в контексте
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
            // Получение связанных записей
            var visitTimeTable = await _context.VisitRegisterTimeTable
                .FirstOrDefaultAsync(vrt => vrt.VisitRegisterId == id);
            var paymentVisit = await _context.PaymentVisit
                .FirstOrDefaultAsync(pv => pv.VisitRegisterId == id);

            // Удаление записей
            if (paymentVisit != null)
            {
                // При отмене действующей записи
                var payment = await _context.Payment.FindAsync(paymentVisit.PaymentId);
                payment.CountRemainTraining += 1;
                _context.PaymentVisit.Remove(paymentVisit);
                _context.Payment.Update(payment);
            }
            if (visitTimeTable != null)
            {
                _context.VisitRegisterTimeTable.Remove(visitTimeTable);
            }
            _context.VisitRegister.Remove(visit);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
