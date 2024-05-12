using ATLANT.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ATLANT.DTO
{
    public class AbonementDTO
    {

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        public decimal Cost { get; set; }
        public int CountVisits { get; set; }
        public int CountDays { get; set; }
        public string TypeService { get; set; }
        public string TypeTraining { get; set; }
    }
}
