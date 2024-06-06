using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ATLANT.DTO
{
    public class VisitRegisterDTO
    {

        public int Id { get; set; }

        public bool VisitDate { get; set; } // false - тренировка ещё не прошла, true - тренировка прошла

        public bool IsPresent { get; set; } // false - пропустил тренировку, true - присутствовал на тренировке 

        public int timetableId { get; set; }
        public int paymentId { get; set; }
    }
}
