using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ATLANT.Models;
using ATLANT.DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ATLANT.Controllers
{
    [Route("api/[controller]")] // добавление к маршруту. То есть можем указать просто Abonements
    [EnableCors]
    [ApiController]
    public class AbonementsController : ControllerBase // ControllerBase - Базовые возможности для Контроллера
    {
        private readonly FitnesContext _context;

        public AbonementsController(FitnesContext context)
        {
            _context = context;
        }

        // GET: api/<AbonementsController>
        // Вывести все абонементы
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Abonement>>> GetAbonements()
        {
            return await _context.Abonement.Include(p => p.Payment).ToListAsync();
        }

        // GET api/<AbonementsController>/5
        // Вывести один абонемент по нужному id
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Abonement>>> GetAbonement(int id)
        {
            var abonement = await _context.Abonement.FindAsync(id);

            if(abonement == null)
            {
                return NotFound();
            }
            return Ok(abonement);
            //return abonement;
        }

        // POST api/<AbonementsController>
        // Добавление нового абонемента
        [HttpPost]
        public async Task<ActionResult<AbonementDTO>> PostAbonement(AbonementDTO abonement)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Abonement abo = new Abonement
            {
                Name = abonement.Name,
                Cost = abonement.Cost,
                CountDays = abonement.CountDays,
                CountVisits = abonement.CountVisits,
                TypeService = abonement.TypeService,
                TypeTraining = abonement.TypeTraining

            };
            _context.Abonement.Add(abo);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<AbonementsController>/5
        // Изменение существующего абонемента
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAbonement(int id, AbonementDTO abonement)
        {
            if(id != abonement.Id)
            {
                return BadRequest();
            }

            var itemAbonement = _context.Abonement.Find(id);
            if (itemAbonement == null)
            {
                return NotFound();
            }

            itemAbonement.Name = abonement.Name;
            itemAbonement.Cost = abonement.Cost;
            itemAbonement.CountDays = abonement.CountDays;
            itemAbonement.CountVisits = abonement.CountVisits;
            itemAbonement.TypeService = abonement.TypeService;
            itemAbonement.TypeTraining = abonement.TypeTraining;
            _context.Abonement.Update(itemAbonement);
            await _context.SaveChangesAsync();
            return Ok(itemAbonement);
            //return NoContent();
        }

        // DELETE api/<AbonementsController>/5
        // Удаление абонемента по id
        [HttpDelete("{id}")]
        [Authorize(Roles ="admin")] // Ограничение доступа (Только для админа)
        public async Task<IActionResult> DeleteAbonement(int id)
        {
            var abonement = await _context.Abonement.FindAsync(id);
            if(abonement == null)
            {
                return NotFound();
            }
            _context.Abonement.Remove(abonement);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
