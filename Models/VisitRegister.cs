using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace ATLANT.Models
{
    public class VisitRegister
    {

        public VisitRegister()
        {
            PaymentVisits = new HashSet<PaymentVisit>();
            VisitRegisterTimeTables = new HashSet<VisitRegisterTimeTable>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public bool VisitDate { get; set; } // false - тренировка ещё не прошла, true - тренировка прошла

        [Required]
        public bool IsPresent { get; set; } // false - пропустил тренировку, true - присутствовал на тренировке 


        public virtual ICollection<VisitRegisterTimeTable> VisitRegisterTimeTables { get; set; }

        public virtual ICollection<PaymentVisit> PaymentVisits { get; set; }

    }
}
