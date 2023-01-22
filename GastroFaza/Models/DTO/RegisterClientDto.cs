using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace GastroFaza.Models.DTO
{
    public class RegisterClientDto
    {

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string Nationality { get; set; } = "Niemcy";
        [DisplayName("Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
    }
}
