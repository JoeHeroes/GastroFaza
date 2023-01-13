using GastroFaza.Models.Enum;

namespace GastroFaza.Models.DTO
{
    public class OrderDetailsDto
    {
        public int ClientId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int OrderId { get; set; }
        public List<Dish> Dishes { get; set; }
        public Status Status { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
