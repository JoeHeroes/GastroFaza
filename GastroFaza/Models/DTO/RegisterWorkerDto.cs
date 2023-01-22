using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class RegisterWorkerDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public string Nationality { get; set; } = "Niemcy";
        [DisplayName("Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
        [DisplayName("Role")]
        public string RoleId { get; set; }
        //1 Kelner 
        //2 Kucharz
        //3 Menadżer
    }
}
