using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class CreateRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool HasDelivery { get; set; }
        public string ContactEmail { get; set; } = null!;
        public string ContactNumber { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string City { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string Street { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
    }
}
