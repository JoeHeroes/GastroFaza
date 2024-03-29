﻿namespace GastroFaza.Models.DTO
{
    public class WorkerDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public int Rating { get; set; }
        public string Role { get; set; }
    }
}
