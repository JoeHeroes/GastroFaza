using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace GastroFaza.Models.DTO
{
    public class ReservationDto
    {
        
        public int[] TableId { get; set; }
        public List<SelectListItem> TableSelect { get; set; }

        [DisplayName("Date Reservation")]
        public DateTime DateOfReservation { get; set; }
        [DisplayName("Hour Reservation")]
        public DateTime HourOfReservation { get; set; }
    }
}
