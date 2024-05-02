using ATLANT.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ATLANT.Data
{
    public class IdentitySeed
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider, FitnesContext context)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            // Создание ролей администратора и пользователя
            if(await roleManager.FindByNameAsync("client") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("client"));
            }
            if (await roleManager.FindByNameAsync("coach") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("coach"));
            }
            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("admin"));
            }

            //Создание администратора
            string adminNickname = "Admin";
            string adminFio = "Админов Админ Админович";
            string adminPhonenumber = "89159161111";
            string adminEmail = "admin@mail.com";
            string adminPassword = "Aa123456!";
            DateTime adminBirthday = new DateTime(2002, 1, 1);
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User UserAdmin = new User {
                    Nickname = adminNickname,
                    FIO = adminFio,
                    PhoneNumber= adminPhonenumber,
                    Birthday = adminBirthday,
                    Email = adminEmail, 
                    UserName = adminEmail
                    };
                IdentityResult result = await userManager.CreateAsync(UserAdmin, adminPassword);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(UserAdmin, "admin");
                }
            }

            // Создание пользователя-клиента
            string userNickname = "SimpleUser";
            string userFio = "Клиентов Клиент Клиентович";
            string userPhonenumber = "89621892870";
            string userEmail = "user@mail.com";
            string userPassword = "Aa123456!";
            DateTime userBirthday = new DateTime(2006, 6, 6);
            if (await userManager.FindByNameAsync(userEmail) == null)
            {
                User UserClient = new User
                { 
                    Nickname = userNickname, 
                    FIO = userFio,
                    PhoneNumber =userPhonenumber, 
                    Birthday = userBirthday,
                    Email = userEmail,
                    UserName = userEmail,
                };

                IdentityResult result = await userManager.CreateAsync(UserClient, userPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(UserClient, "client");
                    Client client = new Client { UserId = UserClient.Id, Balance = 0 };

                    context.Clients.Add(client); // добавляется клиент в список клиентов
                    await context.SaveChangesAsync();
                }
            }

            // Создание пользователя-тренера
            string coachNickname = "SimpleCoach";
            string coachFio = "Тренеров Тренер Тренерович";
            string coachPhonenumber = "89123456789";
            string coachEmail = "coach@mail.com";
            string coachPassword = "Aa123456!";
            DateTime coachBirthday = new DateTime(1998, 7, 12);
            if (await userManager.FindByNameAsync(coachEmail) == null)
            {
                User UserCoach = new User
                { 
                    Nickname = coachNickname,
                    FIO = coachFio,
                    PhoneNumber = coachPhonenumber,
                    Birthday = coachBirthday,
                    Email = coachEmail, 
                    UserName = coachEmail,
                };
             
                IdentityResult result = await userManager.CreateAsync(UserCoach, coachPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(UserCoach, "coach");
                    Coach coach = new Coach { UserId = UserCoach.Id };

                    context.Coachs.Add(coach); // добавляется тренер в список тренеров
                    await context.SaveChangesAsync();

                }
            }
        }
    }
}
