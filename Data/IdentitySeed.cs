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

            // Создание пользователя-тренера Йога
            string coachNickname = "Kristina";
            string coachFio = "Иванова Кристина Михайловна";
            string coachPhonenumber = "89123456789";
            string coachEmail = "coach1@mail.com";
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
                    Coach coach = new Coach { UserId = UserCoach.Id, Description= "Опытный специалист по Йоге. Инструктор фитнеса и бодибилдинга колледжа им. Б. Вейдера\r\nЗолотой знак ГТО V ступени\r\nСеребряный знак ГТО IV ступени\r\nДиплом Шуйского филиала ИВГУ “Физическая культура”",
                        ImageLink = "https://lh3.google.com/u/0/d/1PDKR7yrdtq4L6rwXr_8FFCy0DFZwiJb6"
                    };

                    context.Coachs.Add(coach); // добавляется тренер в список тренеров
                    await context.SaveChangesAsync();

                }
            }

            // Создание пользователя-тренера
            string coachNickname2 = "Anna";
            string coachFio2 = "Королева Анна Сергеевна";
            string coachPhonenumber2 = "89111111111";
            string coachEmail2 = "coach2@mail.com";
            string coachPassword2 = "Aa123456!";
            DateTime coachBirthday2 = new DateTime(1995, 8, 7);
            if (await userManager.FindByNameAsync(coachEmail2) == null)
            {
                User UserCoach = new User
                {
                    Nickname = coachNickname2,
                    FIO = coachFio2,
                    PhoneNumber = coachPhonenumber2,
                    Birthday = coachBirthday2,
                    Email = coachEmail2,
                    UserName = coachEmail2,
                };

                IdentityResult result = await userManager.CreateAsync(UserCoach, coachPassword2);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(UserCoach, "coach");
                    Coach coach = new Coach { UserId = UserCoach.Id, Description = "Опытный специалист по Карате. Бронзовый призер чемпионата мира\r\nВице-чемпионка России\r\nМногократный призер российских региональных турниров",
                        ImageLink = "https://lh3.google.com/u/0/d/1kjd3_Bg7bd1us5xa8CZ2P3QapsXS2Dqe"
                    };

                    context.Coachs.Add(coach); // добавляется тренер в список тренеров
                    await context.SaveChangesAsync();

                }
            }

            // Создание пользователя-тренера
            string coachNickname3 = "Andrey";
            string coachFio3 = "Петров Андрей Алексеевич";
            string coachPhonenumber3 = "89222222222";
            string coachEmail3 = "coach3@mail.com";
            string coachPassword3 = "Aa123456!";
            DateTime coachBirthday3 = new DateTime(1985, 4, 4);
            if (await userManager.FindByNameAsync(coachEmail3) == null)
            {
                User UserCoach = new User
                {
                    Nickname = coachNickname3,
                    FIO = coachFio3,
                    PhoneNumber = coachPhonenumber3,
                    Birthday = coachBirthday3,
                    Email = coachEmail3,
                    UserName = coachEmail3,
                };

                IdentityResult result = await userManager.CreateAsync(UserCoach, coachPassword3);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(UserCoach, "coach");
                    Coach coach = new Coach { UserId = UserCoach.Id, Description = "Опытный специалист по Боксу и Пауэрлифтингу. Участник международных соревнований по ММА fight night global битва на Волге\r\nЧемпион всероссийских соревнований по ММА \"ДУХ ВОИНА\"\r\nЧемпион всероссийских соревнований по армейскому рукопашному бою среди вооружённых сил России\r\nУчастник всероссийских соревнований по универсальному бою",
                        ImageLink = "https://lh3.google.com/u/0/d/1gfZEJ9vtLQltHqYpFJzCxqvUoV655w2Z"
                    };

                    context.Coachs.Add(coach); // добавляется тренер в список тренеров
                    await context.SaveChangesAsync();

                }
            }

            // Создание пользователя-тренера
            string coachNickname4 = "Dwayne";
            string coachFio4 = "Скала Дуэйн Джонсович";
            string coachPhonenumber4 = "89333333333";
            string coachEmail4 = "coach4@mail.com";
            string coachPassword4 = "Aa123456!";
            DateTime coachBirthday4 = new DateTime(1972, 5, 2);
            if (await userManager.FindByNameAsync(coachEmail4) == null)
            {
                User UserCoach = new User
                {
                    Nickname = coachNickname4,
                    FIO = coachFio4,
                    PhoneNumber = coachPhonenumber4,
                    Birthday = coachBirthday4,
                    Email = coachEmail4,
                    UserName = coachEmail4,
                };

                IdentityResult result = await userManager.CreateAsync(UserCoach, coachPassword4);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(UserCoach, "coach");
                    Coach coach = new Coach { UserId = UserCoach.Id, Description = "Опытный специалист по Боксу и Пауэрлифтингу. На протяжении 10 лет работал спасателем Малибу. Мастер спорта по бодибилдингу.",
                        ImageLink = "https://lh3.google.com/u/0/d/1aDHOvp9SE3Bx1Coy0BVmJgnhYb3pXNQI"
                    };

                    context.Coachs.Add(coach); // добавляется тренер в список тренеров
                    await context.SaveChangesAsync();

                }
            }

        }
    }
}
