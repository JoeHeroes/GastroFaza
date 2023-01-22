using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class CreateRestaurantDto
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool HasDelivery { get; set; }
        [Required]
        [DisplayName("Contact Email")]
        public string ContactEmail { get; set; }
        [Required]
        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string City { get; set; }
        [Required]
        [MaxLength(50)]
        public string Street { get; set; }
        [Required]
        [DisplayName("Postal Code")]
        public string PostalCode { get; set; }
    }
}
