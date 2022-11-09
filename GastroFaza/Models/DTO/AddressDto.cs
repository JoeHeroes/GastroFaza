using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class AddresstDto
    {
        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string Street { get; set; } = null!;
        [Required]
        public string PostalCode { get; set; } = null!;
    }
}
