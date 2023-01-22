using System.ComponentModel;

namespace GastroFaza.Models.DTO
{
    public class ReservationWorkerDto
    {
        public int ClientId { get; set; }
        public int[] TableId { get; set; }

        [DisplayName("Date Reservation")]
        public DateTime DateOfReservation { get; set; }
        [DisplayName("Hour Reservation")]
        public DateTime HourOfReservation { get; set; }
    }
}
