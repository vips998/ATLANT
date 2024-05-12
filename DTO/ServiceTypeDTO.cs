using ATLANT.Models;
using System.ComponentModel.DataAnnotations;

namespace ATLANT.DTO
{
    public class ServiceTypeDTO
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string NameService { get; set; }

    }
}
