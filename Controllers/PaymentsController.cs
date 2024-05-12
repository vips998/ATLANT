using ATLANT.DTO;
using ATLANT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATLANT.Controllers
{
    [Route("api/[controller]")] // добавление к маршруту. То есть можем указать просто DayWeeks
    [EnableCors]
    [ApiController]
    public class PaymentsController : Controller
    {
        private readonly FitnesContext _context;

        public PaymentsController(FitnesContext context)
        {
            _context = context;
        }

        // GET: api/<PaymentsController>
        // Вывести все покупки абонементов
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            return await _context.Payment.ToListAsync();
        }


        // GET api/<PaymentsController>/5
        // Вывести одну покупку абонемента по нужному id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayment(int id)
        {
            var payment = await _context.Payment.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        // GET api/<PaymentsController>/5
        // Вывести все купленные абонементы по нужному id клиента и тут же проверка на действительность
        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPaymentsByClient(int clientId)
        {

            // Получение текущей даты
            var today = DateTime.Now;

            var payments = await _context.Payment.Include(p=>p.Abonement).Where(p=>p.UserId == clientId).ToListAsync();

            if (payments == null || !payments.Any())
            {
                return NotFound();
            }

            // Фильтрация недействительных платежей (те, у которых истек срок действия)
            var invalidPayments = payments.Where(p => p.DateEnd < today);

            foreach (var payment in invalidPayments)
            {
                // Обновление флага IsValid для недействительных платежей
                payment.IsValid = false;
                _context.Payment.Update(payment);
            }
            await _context.SaveChangesAsync();

            return Ok(payments);
        }

        // POST api/<PaymentsController>
        // Добавление новой покупки
        [HttpPost]
        public async Task<ActionResult<PaymentDTO>> PostPayment(int clientId, int abonementId, PaymentDTO paymentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Получение клиента по идентификатору
            var client = await _context.Clients.FindAsync(clientId);
            if (client == null)
            {
                return NotFound($"Клиент с данным id {clientId} не найден.");
            }
            var abonement = await _context.Abonement.FindAsync(abonementId);
            if (abonement == null)
            {
                return NotFound($"Абонемент с данным id {abonementId} не найден.");
            }

            // Проверяем, достаточно ли средств на балансе клиента
            if (client.Balance < abonement.Cost)
            {
                return BadRequest($"Недостаточно средств на балансе клиента. Баланс клиента: {client.Balance}, Стоимость абонемента: {abonement.Cost}");
            }
            // Списываем сумму со счета клиента
            client.Balance -= abonement.Cost;
            _context.Clients.Update(client);


            Payment payment = new Payment
            {
                IsValid = paymentDTO.isValid,
                CountRemainTraining = paymentDTO.countRemainTraining,
                DateStart = paymentDTO.dateStart,
                DateEnd = paymentDTO.dateEnd,
                AbonementId = abonementId,
                UserId = clientId
            };

            _context.Payment.Add(payment);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<PaymentsController>/5
        // Изменение существующей покупки абонемента
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, PaymentDTO payment)
        {
            if (id != payment.Id)
            {
                return BadRequest();
            }

            var itemPayment = _context.Payment.Find(id);
            if (itemPayment == null)
            {
                return NotFound();
            }

            itemPayment.IsValid = payment.isValid;
            itemPayment.CountRemainTraining = payment.countRemainTraining;
            itemPayment.DateStart = payment.dateStart;
            itemPayment.DateEnd = payment.dateEnd;

            _context.Payment.Update(itemPayment);
            await _context.SaveChangesAsync();
            return Ok(itemPayment);
            //return NoContent();
        }

        // DELETE api/<PaymentsController>/5
        // Удаление покупки абонемента по id
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Ограничение доступа (Только для админа)
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
