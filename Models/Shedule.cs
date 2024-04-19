using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATLANT.Models
{
    public class Shedule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int MaxCount { get; set; } // Максимальное количество людей

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; } // Сама дата проведения тренировки

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime")]
        public DateTime TimeStart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime")]
        public DateTime TimeEnd { get; set; }

        public int DayWeekId { get; set; }
        public virtual DayWeek DayWeek { get; set; }
        public int CoachId { get; set; }
        public virtual Coach Coach { get; set; }
        public int ServiceTypeId { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public int TypeTrainingId { get; set; }
        public virtual TypeTraining TypeTraining { get; set; }

    }

}
