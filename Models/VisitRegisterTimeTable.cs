using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ATLANT.Models
{
    public class VisitRegisterTimeTable
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("VisitRegister")]
        public int VisitRegisterId { get; set; }

        [ForeignKey("TimeTable")]
        public int TimeTableId { get; set; }

        public virtual VisitRegister VisitRegister { get; set; }
        public virtual TimeTable TimeTable { get; set; }

    }
}
