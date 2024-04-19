using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ATLANT.Models
{
    public class DayWeek
    {
        public DayWeek()
        {
            Shedule = new HashSet<Shedule>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Day { get; set; }

        public virtual ICollection<Shedule> Shedule { get; set; }
    }
}
