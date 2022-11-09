using GastroFaza.Models.Enum;

namespace GastroFaza.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double Price { get; set; }
        public DishType DishType { get; set; }


        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
