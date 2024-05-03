using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ATLANT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Runtime.CompilerServices;

namespace ATLANT.Controllers
{
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly FitnesContext _context; // контекст для добавления Client в список клиентов после регистрации
        private readonly IConfiguration _configuration;  // Добавить это поле

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, FitnesContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _configuration = configuration;
        }

        private string GenerateJwtToken(User user, string role)
        {
            try
            {
                var claims = new List<Claim>
                {
               new Claim(JwtRegisteredClaimNames.Sub, user.Email),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
               new Claim("id", user.Id.ToString()),
               new Claim("userName", user.Nickname.ToString()),
               new Claim("fio", user.FIO.ToString()),
               new Claim("birthday",  user.Birthday.ToShortDateString()),
               new Claim("phonenumber", user.PhoneNumber.ToString()),
               new Claim("email", user.Email.ToString()),
               new Claim("userRole", role.ToString()),
                };
                // Добавляем баланс только для клиентов
                if (role == "client")
                {
                    var client = _context.Clients.FirstOrDefault(c => c.UserId == user.Id);
                    if (client != null)
                    {
                        claims.Add(new Claim("clientBalance", client.Balance.ToString()));
                    }
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["Jwt:ExpireDays"]));

                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: expires,
                    signingCredentials: creds
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                Console.WriteLine("Ошибка при генерации JWT: " + ex.Message);
                return null;
            }
        }


        // POST: AccountController/register
        [HttpPost]
        [Route("api/account/register")]
        [AllowAnonymous]

        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                User user = new()
                {
                    Nickname = model.Nickname,
                    FIO = model.FIO,
                    PhoneNumber = model.PhoneNumber,
                    Birthday = model.Birthday,
                    Email = model.Email,
                    UserName = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Установка роли
                    await _userManager.AddToRoleAsync(user, "client");

                    // Создаем клиента с балансом 0 рублей
                    Client client = new Client
                    {
                        UserId = user.Id,
                        Balance = 0
                    };
                    _context.Clients.Add(client);
                    await _context.SaveChangesAsync();

                    // Установка куки
                    await _signInManager.SignInAsync(user, false);

                    IList<string> roles = await _userManager.GetRolesAsync(user);
                    string? userRole = roles.FirstOrDefault();

                    return Ok(new { message = "Добавлен новый пользователь: ", user.Id, userName = user.Nickname, userRole, clientBalance = client.Balance, fio = user.FIO, birthday = user.Birthday, phonenumber=user.PhoneNumber, email=user.Email });    
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);  
                    }
                    var errorMsg = new
                    {
                        message = "Пользователь не добавлен",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };
                    return Created("", errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Неверные входные данные",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                };
                return Created("", errorMsg);
            }
        }

        [HttpPost]
        [Route("api/account/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid) 
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if(result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    IList<string>? roles = await _userManager.GetRolesAsync(user);
                    string? userRole = roles.FirstOrDefault();
                    var token = GenerateJwtToken(user, userRole);
                    if (token == null)
                    {
                        return StatusCode(500, "Ошибка при создании токена");
                    }

                    // Если клиент авторизовался
                    if (userRole == "client")
                    {
                        var client = await _context.Clients.FirstOrDefaultAsync(c => c.UserId == user.Id);
                        if (client != null)
                        {
                            return Ok(new { token, message = "Выполнен вход", user.Id, userName = user.Nickname, userRole, clientBalance = client.Balance, fio = user.FIO, birthday = user.Birthday, phonenumber = user.PhoneNumber, email = user.Email });
                        }
                        else
                        {
                            return Ok(new { token, message = "Выполнен вход, но информация о клиенте не найдена", user.Id, userName = user.Nickname, userRole });
                        }
                    }
                    // Если тренер авторизовался
                    if(userRole == "coach")
                    {
                        var coach = await _context.Coachs.FirstOrDefaultAsync(c => c.UserId == user.Id);
                        if(coach != null)
                        {
                            return Ok(new { token, message = "Выполнен вход", user.Id, userName = user.Nickname, userRole, fio = user.FIO, birthday = user.Birthday, phonenumber = user.PhoneNumber, email = user.Email });
                        }
                        else
                        {
                            return Ok(new { token, message = "Выполнен вход, но информация о тренере не найдена", user.Id, userName = user.Nickname, userRole });
                        }
                    }

                    // Если админ авторизовался
                    if (userRole == "admin")
                    {
                        {
                            return Ok(new { token, message = "Выполнен вход", user.Id, userName = user.Nickname, userRole, fio = user.FIO, birthday = user.Birthday, phonenumber = user.PhoneNumber, email = user.Email });
                        }
                    }


                    return Ok(new { token, message = "Выполнен вход", user.Id, userName = user.Nickname, userRole, fio = user.FIO, birthday = user.Birthday, phonenumber = user.PhoneNumber, email = user.Email });
                }

                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                    var errorMsg = new
                    {
                        message = "Вход не выполнен",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };
                    return Created("", errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Вход не выполнен",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                };
                return Created("", errorMsg);
            }
        }

        [HttpPost]
        [Route("api/account/logoff")]
        public async Task<IActionResult> LogOff()
        {
            User usr = await GetCurrentUserAsync();
            if(usr == null) 
            {
                return Unauthorized(new { message = "Сначала выполните вход" });
            }
            // Удаление куки
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Выполнен выход", userName = usr.UserName });
        }

        [HttpGet]
        [Route("api/account/isauthenticated")]
        public async Task<IActionResult> IsAuthenticated()
        {
            User usr = await GetCurrentUserAsync();
            if(usr == null)
            {
                return Unauthorized(new {message = "Вы гость. Пожалуйста, выполните вход"});
            }
            IList<string> roles = await _userManager.GetRolesAsync(usr);
            string? userRole = roles.FirstOrDefault();
            return Ok(new { message = "Сессия активна", userName = usr.UserName, userRole });
        }
        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}
