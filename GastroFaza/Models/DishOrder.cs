namespace GastroFaza.Models
{
    public class DishOrder
    {
        public int DishesId { get; set; }
        public int OrderId { get; set; }

        public Dish Dish { get; set; }
        public Order Order { get; set; }


    }
}
