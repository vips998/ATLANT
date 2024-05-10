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
    public class TypeTrainingsController : Controller
    {
            private readonly FitnesContext _context;

            public TypeTrainingsController(FitnesContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<TypeTraining>>> GetTypeTrainings()
            {
                return await _context.TypeTraining.ToListAsync();
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<IEnumerable<TypeTraining>>> GetTypeTraining(int id)
            {
                var typeTraining = await _context.TypeTraining.FindAsync(id);

                if (typeTraining == null)
                {
                    return NotFound();
                }
                return Ok(typeTraining);
            }

        // POST api/<TypeTrainingsController>
        // Добавление нового типа тренировки
        [HttpPost]
        public async Task<ActionResult<TypeTrainingDTO>> PostTypeTraining(TypeTrainingDTO type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TypeTraining typetr = new TypeTraining
            {
                 NameType = type.NameType,
            };
            _context.TypeTraining.Add(typetr);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<TypeTrainingsController>/5
        // Изменение существующего типа тренировки
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTypeTraining(int id, TypeTrainingDTO type)
        {
            if (id != type.Id)
            {
                return BadRequest();
            }

            var itemTypeTraining = _context.TypeTraining.Find(id);
            if (itemTypeTraining == null)
            {
                return NotFound();
            }

            itemTypeTraining.NameType = type.NameType;
            _context.TypeTraining.Update(itemTypeTraining);
            await _context.SaveChangesAsync();
            return Ok(itemTypeTraining);
            //return NoContent();
        }

        // DELETE api/<TypeTrainingsController>/5
        // Удаление услуги по id
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Ограничение доступа (Только для админа)
        public async Task<IActionResult> DeleteTypeTraining(int id)
        {
            var type = await _context.TypeTraining.FindAsync(id);
            if (type == null)
            {
                return NotFound();
            }
            _context.TypeTraining.Remove(type);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
