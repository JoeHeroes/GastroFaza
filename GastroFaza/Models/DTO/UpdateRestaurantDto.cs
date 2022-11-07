using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class UpdateRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool HasDelivery { get; set; }
    }
}
