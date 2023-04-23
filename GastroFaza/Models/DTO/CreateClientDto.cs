using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class CreateClientDto
    {
        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Please provide the invitee's Email Address")]
        [EmailAddress(ErrorMessage = "Please provide a valid email address")]
        [StringLength(254, ErrorMessage = "Maximum email address length exceeded")]
        public string Email { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        [DisplayName("Phone Number")]
        public int PhoneNumber { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public int OrderID { get; set; }
    }
}
