using ATLANT.DTO;
using ATLANT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATLANT.Controllers
{
    [Route("api/[controller]")] // добавление к маршруту. То есть можем указать просто Coachs
    [EnableCors]
    [ApiController]
    public class ServiceTypesController : Controller
    {
        private readonly FitnesContext _context;

        public ServiceTypesController(FitnesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceType>>> GetServiceTypes()
        {
            return await _context.ServiceType.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ServiceType>>> GetServiceType(int id)
        {
            var serviceType = await _context.ServiceType.FindAsync(id);

            if (serviceType == null)
            {
                return NotFound();
            }
            return Ok(serviceType);
        }

        // POST api/<ServiceTypesController>
        // Добавление новой услуги
        [HttpPost]
        public async Task<ActionResult<ServiceTypeDTO>> PostServiceType(ServiceTypeDTO service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceType serviceTYPE = new ServiceType
            {
                NameService = service.NameService,
            };
            _context.ServiceType.Add(serviceTYPE);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/<ServiceTypesController>/5
        // Изменение существующей услуги
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServiceType(int id, ServiceTypeDTO service)
        {
            if (id != service.Id)
            {
                return BadRequest();
            }

            var itemServiceType = _context.ServiceType.Find(id);
            if (itemServiceType == null)
            {
                return NotFound();
            }

            itemServiceType.NameService = service.NameService;
            _context.ServiceType.Update(itemServiceType);
            await _context.SaveChangesAsync();
            return Ok(itemServiceType);
            //return NoContent();
        }

        // DELETE api/<ServiceTypeController>/5
        // Удаление услуги по id
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Ограничение доступа (Только для админа)
        public async Task<IActionResult> DeleteServiceType(int id)
        {
            var service = await _context.ServiceType.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            _context.ServiceType.Remove(service);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
