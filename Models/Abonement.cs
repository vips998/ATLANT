﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATLANT.Models
{
    public class Abonement
    {
        public Abonement()
        {
            PaymentAbonements = new HashSet<PaymentAbonement>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Cost { get; set; }
        public int CountVisits { get; set; }
        public int CountDays { get; set; }

        [Required]
        public string TypeService { get; set; }
        [Required]

        public virtual ICollection<PaymentAbonement> PaymentAbonements { get; set; }
    }
}
