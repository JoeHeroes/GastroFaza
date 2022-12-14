namespace GastroFaza.Models
{
    public class DiningTable
    {
        public int Id { get; set; }
        public bool Busy { get; set; }
        public int Seats { get; set; } = 4;
        public int ClientId { get; set; }

    }
}
