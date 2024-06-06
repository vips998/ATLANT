using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATLANT.Models
{
    public class PaymentVisit
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("Payment")]
        public int PaymentId { get; set; }

        [ForeignKey("VisitRegister")]
        public int VisitRegisterId { get; set; }

        public virtual VisitRegister VisitRegister { get; set; }

        public virtual Payment Payment { get; set; }

    }
}
