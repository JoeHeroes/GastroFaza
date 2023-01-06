using Microsoft.AspNetCore.Mvc.Rendering;

namespace GastroFaza.Models.DTO
{
    public class EditClientDto 
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public List<SelectListItem> SelectedNations { get; set; }
    }
}
