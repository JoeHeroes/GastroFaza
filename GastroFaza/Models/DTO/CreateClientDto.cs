using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class CreateClientDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public int OrderID { get; set; }
    }
}
