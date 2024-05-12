using ATLANT.Models;
using System.ComponentModel.DataAnnotations;

namespace ATLANT.DTO
{
    public class TypeTrainingDTO
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string NameType { get; set; }

    }
}
