using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATLANT.Models
{
    public class Payment
    {
        public Payment()
        {
            PaymentVisit = new HashSet<PaymentVisit>();
            PaymentAbonement = new HashSet<PaymentAbonement>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime DateStart { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime DateEnd { get; set; }
        [Required]
        public int CountRemainTraining { get; set; }

        [Required]
        public bool IsValid { get; set; }

        [ForeignKey("Client")]
        public int UserId { get; set; }
        public virtual Client Client { get; set; }

        public virtual ICollection<PaymentAbonement> PaymentAbonement { get; set; }
        public virtual ICollection<PaymentVisit> PaymentVisit { get; set; }

    }
}
