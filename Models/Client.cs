using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ATLANT.Models
{
    public class Client 
    {
        public Client()
        {
            Payment = new HashSet<Payment>();
        }
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Balance { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }

        public User? User { get; set; }

    }
}
