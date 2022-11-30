namespace GastroFaza.Models
{
    public class Order
    {


        public Order()
        {
            this.Dishes = new HashSet<Dish>();
        }
        public int Id { get; set; }
        public virtual ICollection<Dish> Dishes { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int AddedById { get; set; }
    }
}
