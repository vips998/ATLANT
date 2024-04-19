using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;

namespace ATLANT.Models
{
    public class User: IdentityUser<int>
    {

        [StringLength(50)]
        public string? Nickname { get; set; }

        [Required]
        [StringLength(50)]
        public string? FIO { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Birthday { get; set; }

        public Client? Client { get; set; }
        public Coach? Coach { get; set; }
    }
}
