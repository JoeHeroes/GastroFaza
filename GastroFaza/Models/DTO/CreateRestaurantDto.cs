using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class CreateRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public bool HasDelivery { get; set; }
        [Required]
        public string ContactEmail { get; set; } = null!;
        [Required]
        public string ContactNumber { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string City { get; set; } = null!;
        [Required]
        [MaxLength(50)]
        public string Street { get; set; } = null!;
        [Required]
        public string PostalCode { get; set; } = null!;
    }
}
