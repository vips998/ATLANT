using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ATLANT.DTO
{
    public class TimeTableDTO
    {

        [Required]
        public int MaxCount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime")]
        public DateTime TimeStart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "datetime")]
        public DateTime TimeEnd { get; set; }
        public int CoachId { get; set; }
        public int ServiceTypeId { get; set; }
        public int TypeTrainingId { get; set; }
    }
}
