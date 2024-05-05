using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ATLANT.DTO
{
    public class PaymentDTO
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime dateStart { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime dateEnd { get; set; }
        public int countRemainTraining { get; set; }

        public bool isValid { get; set; }
    }
}
