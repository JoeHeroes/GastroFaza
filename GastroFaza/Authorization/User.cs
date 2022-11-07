using System;

namespace GastroFaza.Authorization
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; } = "";
        public string PasswordHash { get; set; } = null!;
    }
}
