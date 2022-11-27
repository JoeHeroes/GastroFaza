namespace GastroFaza.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<Dish> Dishes { get; set; } = new List<Dish>();
        public string Description { get; set; }
        public double Price { get; set; }
        public int AddedById { get; set; }
    }
}
