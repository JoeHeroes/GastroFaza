using GastroFaza.Models.Enum;
using System.ComponentModel;

namespace GastroFaza.Models.DTO
{
    public class OrderDetailsDto
    {
        public int ClientId { get; set; }
        public string Email { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public int OrderId { get; set; }
        public List<Dish> Dishes { get; set; }
        public Status Status { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
