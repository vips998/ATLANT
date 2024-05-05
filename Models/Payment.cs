using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATLANT.Models
{
    public class Payment
    {
        public Payment()
        {
            VisitRegister = new HashSet<VisitRegister>();
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

        [ForeignKey("Abonement")]
        public int AbonementId { get; set; }
        public virtual Abonement Abonement { get; set; }

        public virtual ICollection<VisitRegister> VisitRegister { get; set; }

    }
}
