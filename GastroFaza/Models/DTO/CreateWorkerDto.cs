using GastroFaza.Authorization;
using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class CreateWorkerDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string FirstName { get; set; } = "";
        [Required]
        public string LastName { get; set; } = "";
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string Nationality { get; set; } = "";
        [Required]
        public string PasswordHash { get; set; } = null!;
        [Required]
        public float Salary { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required] 
        public int RoleId { get; set; }

    }
}