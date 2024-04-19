using ATLANT.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq.Expressions;

namespace ATLANT.Data
{
    public class FitnesContextSeed
    {
        public static async Task SeedAsync(FitnesContext context)
        {
            try
            {
                context.Database.EnsureCreated();
                if (context.Abonement.Any()) // если содержит все элементы то возврат
                {
                    return;
                }

                var servicestypes = new ServiceType[]
                {
 new ServiceType { NameService = "Йога"},
 new ServiceType {NameService = "Бокс"}
                };
                foreach (ServiceType st in servicestypes)
                {
                    context.ServiceType.Add(st);
                }
                await context.SaveChangesAsync();

                var typetraining = new TypeTraining[]
                {
 new TypeTraining {NameType = "Групповая"},
 new TypeTraining {NameType = "Индивидуальная"}
                };
                foreach (TypeTraining t in typetraining)
                {
                    context.TypeTraining.Add(t);
                }
                await context.SaveChangesAsync();

                var users = new User[]
                {
 new User {FIO = "Карпов Владислав Дмитриевич", Birthday = new DateTime(2002,07,20),
PhoneNumber = "89621892871", Email="vips998@mail.ru"},
 new User { FIO = "Замыцкий Илья Сергеевич", Birthday = new DateTime(2002,08,02),
PhoneNumber = "88005553535", Email="zam123@mail.ru"},
 new User { FIO = "Комаров Валерий Алексеевич", Birthday = new DateTime(2002,06,25),
PhoneNumber = "89159162232", Email="komarVal@mail.ru"}
            };
                foreach (User f in users)
                {
                    context.Users.Add(f);
                }
                await context.SaveChangesAsync();


                var clients = new Client[]
                {
 new Client {User = users[1], Balance = 10000},
            };
                foreach (Client g in clients)
                {
                    context.Clients.Add(g);
                }
                await context.SaveChangesAsync();

                var coachs = new Coach[]
                {
 new Coach {User = users[2]},
            };
                foreach (Coach c in coachs)
                {
                    context.Coachs.Add(c);
                }
                await context.SaveChangesAsync();

                var payments = new Payment[]
                {
new Payment{NumberCard="33212459", CountRemainTraining=10,
    DateStart=new DateTime(2024-03-01),DateEnd=new DateTime(2024-06-01)},
new Payment{NumberCard="12345678", CountRemainTraining=15,
    DateStart=new DateTime(2024-01-01),DateEnd=new DateTime(2024-06-01)}
                };

                foreach (Payment p in payments)
                {
                    context.Payment.Add(p);
                }
                await context.SaveChangesAsync();

                var abonements = new Abonement[]
                {
 new Abonement
 {Name = "Месячный", Cost=2000,CountDays = 30, CountMonths=1, CountVisits = 100, TypeService="Йога", TypeTraining = "Групповая", Payment = payments},
 new Abonement
 {Name = "3-х месячный", Cost=5000,CountDays = 100, CountMonths=3, CountVisits = 100, TypeService="Бокс", TypeTraining = "Групповая"},
 new Abonement
{Name = "10 дней", Cost=1000,CountDays = 10, CountMonths=0, CountVisits = 10, TypeService="Тренажерный зал", TypeTraining = "Индивидуальная"}
            };
                foreach (Abonement a in abonements)
                {
                    context.Abonement.Add(a);
                }
                await context.SaveChangesAsync();

                var daysweek = new DayWeek[]
                {
new DayWeek{ Day = "Понедельник"},new DayWeek{ Day = "Вторник"},new DayWeek{ Day = "Среда"},
new DayWeek{ Day = "Четверг"},new DayWeek{ Day = "Пятница"},new DayWeek{ Day = "Суббота"},
new DayWeek{ Day = "Воскресение"}
                };

                foreach (DayWeek dw in daysweek)
                {
                    context.DayWeek.Add(dw);
                }
                await context.SaveChangesAsync();

                var shedules = new Shedule[]
{
new Shedule{ MaxCount = 10, Date=new DateTime(2024, 4, 8), TimeStart= new DateTime(2024, 4, 8, 9, 0, 0), 
    TimeEnd = new DateTime(2024, 4, 8, 11, 0, 0), DayWeek = daysweek[0], Coach=coachs[0], ServiceType = servicestypes[0],
 TypeTraining = typetraining[0]},
new Shedule{ MaxCount = 10, Date=new DateTime(2024, 4, 8), TimeStart= new DateTime(2024, 4, 8, 11, 0, 0),
    TimeEnd = new DateTime(2024, 4, 8, 13, 0, 0), DayWeek = daysweek[0], Coach=coachs[0], ServiceType = servicestypes[1],
 TypeTraining = typetraining[1]},
new Shedule{ MaxCount = 10, Date=new DateTime(2024, 4, 8), TimeStart= new DateTime(2024, 4, 8, 13, 0, 0),
    TimeEnd = new DateTime(2024, 4, 8, 15, 0, 0), DayWeek = daysweek[0], Coach=coachs[0], ServiceType = servicestypes[0],
 TypeTraining = typetraining[0]},
new Shedule{ MaxCount = 10, Date=new DateTime(2024, 4, 9), TimeStart= new DateTime(2024, 4, 9, 9, 0, 0),
    TimeEnd = new DateTime(2024, 4, 9, 12, 0, 0), DayWeek = daysweek[1], Coach=coachs[0], ServiceType = servicestypes[1],
 TypeTraining = typetraining[0]},
new Shedule{ MaxCount = 10, Date=new DateTime(2024, 4, 10), TimeStart= new DateTime(2024, 4, 10, 9, 0, 0),
    TimeEnd = new DateTime(2024, 4, 10, 12, 0, 0), DayWeek = daysweek[2], Coach=coachs[0], ServiceType = servicestypes[0],
 TypeTraining = typetraining[0]},
};

                foreach (Shedule sh in shedules)
                {
                    context.Shedule.Add(sh);
                }
                await context.SaveChangesAsync();



            }
            catch
            {
                throw;
            }
        }
    }
}
