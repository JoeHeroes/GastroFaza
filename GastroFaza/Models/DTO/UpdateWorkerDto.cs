using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class UpdateWorkerDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public float Salary { get; set; }

        [DisplayName("Role")]
        public int RoleId { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
    }
}