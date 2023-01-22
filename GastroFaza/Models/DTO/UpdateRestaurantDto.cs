using System.ComponentModel;
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
        [DisplayName("Contact Email")]
        public string ContactEmail { get; set; }
        [PhoneAttribute]
        [DisplayName("Contact Number")]
        public string ContactNumber { get; set; }
    }
}
