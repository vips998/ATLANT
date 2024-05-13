using ATLANT.DTO;
using ATLANT.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATLANT.Controllers
{
    [Route("api/[controller]")] // добавление к маршруту. То есть можем указать просто Coachs
    [EnableCors]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly FitnesContext _context;

        public UsersController(FitnesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.Include(p => p.Client).Include(p => p.Coach).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateBalance(int userId, [FromBody] BalanceUpdateDTO balanceUpdate)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.UserId == userId);

            if (client == null)
            {
                return NotFound();
            }

            client.Balance = balanceUpdate.NewBalance;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return NoContent();
        }

        /*// POST api/<UsersController>
        // Добавление нового тренера
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostCoach(UserDTO newuser, )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ServiceType serviceTYPE = new ServiceType
            {
                NameService = service.NameService,
                Description = service.Description,
                ImageLink = "https://lh3.google.com/u/0/d/",
            };
            _context.ServiceType.Add(serviceTYPE);
            await _context.SaveChangesAsync();
            return Ok(serviceTYPE);
        }*/
    }
}
