using ATLANT.Models;
using Microsoft.AspNetCore.Identity;

namespace ATLANT.Data
{
    public class IdentitySeed
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            // Создание ролей администратора и пользователя
            if(await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole<int>("user"));
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
            string adminEmail = "admin@mail.com";
            string adminPassword = "Aa123456!";
            string adminFio = "Админов Админ Админович";
            DateTime adminBirthday = new DateTime(2002, 1, 1);
            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail, FIO=adminFio, Birthday=adminBirthday };
                IdentityResult result = await userManager.CreateAsync(admin, adminPassword);
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }

            // Создание пользователя
            string userEmail = "user@mail.com";
            string userPassword = "Aa123456!";
            string userFio = "Клиентов Клиент Клиентович";
            DateTime userBirthday = new DateTime(2006, 6, 6);
            //string userNickname = "SimpleUser";
            if (await userManager.FindByNameAsync(userEmail) == null)
            {
                Client client = new Client { Balance = 0 };
                User user = new User { Email = userEmail, UserName = userEmail, FIO = userFio, Birthday = userBirthday,
                Client = client};
                IdentityResult result = await userManager.CreateAsync(user, userPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "user");
                }
            }

            // Создание тренера
            string coachEmail = "coach@mail.com";
            string coachPassword = "Aa123456!";
            string coachFio = "Тренеров Тренер Тренерович";
            DateTime coachBirthday = new DateTime(1998, 7, 12);
            //string userNickname = "SimpleUser";
            if (await userManager.FindByNameAsync(coachEmail) == null)
            {
                Coach trener = new Coach {};
                User coach = new User { Email = userEmail, UserName = coachEmail, FIO = coachFio, Birthday = coachBirthday,
                Coach = trener};
                IdentityResult result = await userManager.CreateAsync(coach, coachPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(coach, "coach");
                }
            }
        }
    }
}
