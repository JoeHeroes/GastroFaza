namespace GastroFaza.Models
{
    public class Tablee
    {
        public int Id { get; set; }
        public bool Busy { get; set; }
        public bool Reserved { get; set; }
        public int Seats { get; set; } = 4;
        public int ClientId { get; set; }

    }
}
