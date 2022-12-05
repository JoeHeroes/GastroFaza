using GastroFaza.Models.Enum;

namespace GastroFaza.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DishType DishType { get; set; }
        public string ProfileImg { get; set; }
        public List<DishOrder> Orders { get; set; }
    }
}
