using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class RegisterClientDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        [Required]
        public string FirstName { get; set; } 
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }

    }
}
