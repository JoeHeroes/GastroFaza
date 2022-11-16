using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class AddressDto
    {
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string PostalCode { get; set; }
    }
}
