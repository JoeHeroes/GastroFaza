using Microsoft.AspNetCore.Mvc.Rendering;


namespace GastroFaza.Models.DTO
{
    public class RegisterClientDto
    {

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string FirstName { get; set; } 

        public string LastName { get; set; }

        public List<SelectListItem> SelectedNations { get; set; }

        public string Nationality { get; set; }

        public DateTime? DateOfBirth { get; set; }

    }
}
