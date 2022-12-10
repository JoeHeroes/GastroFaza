namespace GastroFaza.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int TableId { get; set; }
        public DateTime DataOfReservation{ get; set; }
    }
}
