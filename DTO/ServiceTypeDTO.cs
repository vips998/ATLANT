using ATLANT.Models;
using System.ComponentModel.DataAnnotations;

namespace ATLANT.DTO
{
    public class ServiceTypeDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string NameService { get; set; }

    }
}
