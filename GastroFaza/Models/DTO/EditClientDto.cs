using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class EditClientDto 
    {
        public string Email { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
        [DisplayName("Phone Number")]
        public int PhoneNumber { get; set; }
        public string Nationality { get; set; }
        public List<SelectListItem> SelectedNations { get; set; }
    }
}
