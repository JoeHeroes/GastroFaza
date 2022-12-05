namespace GastroFaza.Models
{
    public class DishOrder
    {

        public int Id { get; set; }


        public int DishesId { get; set; }
        public int OrderId { get; set; }

        public Dish DishMany { get; set; }
        public Order OrderMany { get; set; }


    }
}
