namespace GastroFaza.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string TableId { get; set; }
        public DateTime DataOfReservation{ get; set; }
    }
}
