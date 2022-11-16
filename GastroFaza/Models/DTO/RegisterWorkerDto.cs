namespace GastroFaza.Models.DTO
{
    public class RegisterWorkerDto
    {
        public string Email { get; set; }
        public string Password { get; set; } 
        public string ConfirmPassword { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int RoleId { get; set; } = 1;
        //1 Kelner 
        //2 Kucharz
        //3 Menadżer
    }
}
