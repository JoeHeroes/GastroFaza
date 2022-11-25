using GastroFaza.Models.Enum;

namespace GastroFaza.Models.DTO
{
    public class OptionFilterDto
    {
        public string SearchString { get; set; } = "";
        public DishType Dish { get; set; } = DishType.none;
        public float MinPrice { get; set; } = 0;
        public float MaxPrice { get; set; } = 100;

    }
}
