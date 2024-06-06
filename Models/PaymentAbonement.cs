using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATLANT.Models
{
    public class PaymentAbonement
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Payment")]
        public int PaymentId { get; set; }

        [ForeignKey("Abonement")]
        public int AbonementId { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual Abonement Abonement { get; set; }
    }
}
