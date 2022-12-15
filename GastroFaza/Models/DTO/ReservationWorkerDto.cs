namespace GastroFaza.Models.DTO
{
    public class ReservationWorkerDto
    {
        public int ClientId { get; set; }
        public int[] TableId { get; set; }
        public DateTime DateOfReservation { get; set; }
        public DateTime HourOfReservation { get; set; }


    }
}
