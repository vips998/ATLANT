﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace ATLANT.Models
{
    public class VisitRegister
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public bool VisitDate { get; set; } // false - тренировка ещё не прошла, true - тренировка прошла

        [Required]
        public bool IsPresent { get; set; } // false - пропустил тренировку, true - присутствовал на тренировке 

        [ForeignKey("TimeTable")]
        public int TimeTableId { get; set; }
        public virtual TimeTable TimeTable { get; set; }

        [ForeignKey("Payment")]
        public int PaymentId { get; set; }
        public virtual Payment Payment { get; set; }

    }
}
