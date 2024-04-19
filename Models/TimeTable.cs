﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATLANT.Models
{
    public class TimeTable
    {
        public TimeTable()
        {
            VisitRegister = new HashSet<VisitRegister>();
        }
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

        public virtual ICollection<VisitRegister> VisitRegister { get; set; }


    }
}
