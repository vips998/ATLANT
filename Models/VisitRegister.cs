using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace ATLANT.Models
{
    public class VisitRegister
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int VisitNumber { get; set; }

    }
}
