using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class UpdateRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasDelivery { get; set; }
        [EmailAddress]
        public string ContactEmail { get; set; }
        [PhoneAttribute]
        public string ContactNumber { get; set; }
    }
}
