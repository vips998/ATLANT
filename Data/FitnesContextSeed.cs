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
 new ServiceType { NameService = "Йога", ImageLink="https://lh3.google.com/u/0/d/1W5vO5oCc6uk2-2k07Bw9gGF9_2mb2VAq", Description="Древняя духовная практика, пришедшая к нам из Индии. Она состоит из восьми ступеней, затрагивающих все аспекты человеческого бытия – от соблюдения моральных принципов, работы с телом и с дыханием, до более тонких техник управления психикой, умом и сознанием."},
 new ServiceType {NameService = "Бокс",ImageLink="https://lh3.google.com/u/0/d/1nhG4rQ5ljU3xRUm_yAXlgmQY0LRtcHUF", Description="Смысл этого боевого искусства заключается в том, чтобы любой человек с помощью специальной подготовки, наработанных навыков, скорости и выносливости смог побеждать более крупных по весу и силе противников, уверенно противостоять группе нападающих, легко уходить от ударов и проводить молниеносные атаки."},
 new ServiceType {NameService="Карате", ImageLink="https://lh3.google.com/u/0/d/1HUGejrXHKxeYLIht0YPjAchG9KAM29W4", Description="Популярное боевое искусство, которое направлено на изучение приемов самообороны. Тренировки состоят из активных движений руками и ногами, прыжков, изучения системы блоков для самозащиты и других элементов. Коме того, в карате внимание уделяется навыкам концентрации и психологической подготовке."},
 new ServiceType {NameService="Пауэрлифтинг", ImageLink="https://lh3.google.com/u/0/d/1gMa-uoCtBAVuMzjvmpVFYMc_m9fHwBiT",Description="Популярное силовое троеборье, потому что включает в себя три основных упражнения: приседания со штангой на плечах, жим лежа и становую тягу. Все эти упражнения направлены на то, чтобы показать физическую форму спортсмена, а также его силовые качества."}    
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

                var abonements = new Abonement[]
                {
 new Abonement
 {Name = "Месячный", Cost=2000,CountDays = 30, CountVisits = 100, TypeService="Йога"},
 new Abonement
 {Name = "3-х месячный", Cost=5000,CountDays = 100, CountVisits = 100, TypeService="Бокс"},
 new Abonement
{Name = "10 дней", Cost=1000,CountDays = 10, CountVisits = 10, TypeService="Карате"},
 new Abonement
{Name = "Неделя", Cost=700,CountDays = 7, CountVisits = 7, TypeService="Пауэрлифтинг"}
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



            }
            catch
            {
                throw;
            }
        }
    }
}
