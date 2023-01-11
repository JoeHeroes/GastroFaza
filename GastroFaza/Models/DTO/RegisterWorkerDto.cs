using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class RegisterWorkerDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; } 
        public string ConfirmPassword { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<SelectListItem> SelectedNations { get; set; }
        public List<SelectListItem> Roles { get; set; }
        public string Nationality { get; set; } = "Niemcy";
        public DateTime? DateOfBirth { get; set; }
        public string RoleId { get; set; }
        //1 Kelner 
        //2 Kucharz
        //3 Menadżer
    }
}
