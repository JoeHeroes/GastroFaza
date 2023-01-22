using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class CreateWorkerDto
    {
        [Required]
        [EmailAddress]
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
        public string Nationality { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public float Salary { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        [DisplayName("Role")]
        public int RoleId { get; set; }

        public List<SelectListItem> Roles { get; set; }

        public SelectListItem Role { get; set; }

        public List<SelectListItem> SelectedNations { get; set; }
       
        public SelectListItem SelectedNation { get; set; }

    }
}