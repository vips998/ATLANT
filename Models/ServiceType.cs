using System.ComponentModel.DataAnnotations;

namespace ATLANT.Models
{
    public class ServiceType
    {
        public ServiceType()
        {
            TimeTable = new HashSet<TimeTable>();
            Shedule = new HashSet<Shedule>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NameService { get; set; }

        public virtual ICollection<TimeTable> TimeTable { get; set; }

        public virtual ICollection<Shedule> Shedule { get; set; }
    }
}
