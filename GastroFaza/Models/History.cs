namespace GastroFaza.Models
{
    public class History
    {
        public int Id { get; set; }
        public string Dishes { get; set; }
        public DateTime Date { get; set; }
        public int AddedById { get; set; }

        public int Stars { get; set; }

    }
}
