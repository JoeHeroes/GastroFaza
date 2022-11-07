namespace GastroFaza.Models.DTO
{
    public class RegisterClientDto
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Nationality { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }

    }
}
