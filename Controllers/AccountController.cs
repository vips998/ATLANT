﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ATLANT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ATLANT.Controllers
{
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly FitnesContext _context; // контекст для добавления Client в список клиентов после регистрации

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, FitnesContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context; 
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
                    UserName = model.Nickname,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Установка роли
                    await _userManager.AddToRoleAsync(user, "user");

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
                    return Ok(new { message = "Добавлен новый пользователь: " + user.UserName });    
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

                    // Если клиент авторизовался
                    if (userRole == "client")
                    {
                var client = await _context.Clients.FirstOrDefaultAsync(c => c.UserId == user.Id);
                if (client != null)
                {
                    return Ok(new { message = "Выполнен вход", user.Id, userName = user.Nickname, userRole, clientBalance = client.Balance });
                }
                else
                {
                    return Ok(new { message = "Выполнен вход, но информация о клиенте не найдена", user.Id, userName = user.Nickname, userRole });
                }
                    }


                    return Ok(new { message = "Выполнен вход", user.Id, userName = user.Nickname, userRole });
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
