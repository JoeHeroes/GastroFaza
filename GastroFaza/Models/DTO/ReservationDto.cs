using Microsoft.AspNetCore.Mvc.Rendering;

namespace GastroFaza.Models.DTO
{
    public class ReservationDto
    {
        
        public int[] TableId { get; set; }
        public List<SelectListItem> TableSelect { get; set; }
        public DateTime DateOfReservation { get; set; }
        public DateTime HourOfReservation { get; set; }
    }
}
