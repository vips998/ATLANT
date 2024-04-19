using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace ATLANT.Models
{
    public class Coach
    {
        public Coach()
        {
            TimeTable = new HashSet<TimeTable>();
            Shedule = new HashSet<Shedule>();
        }
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual ICollection<TimeTable> TimeTable { get; set; }

        public virtual ICollection<Shedule> Shedule { get; set; }
        public User? User { get; set; }
    }
}
