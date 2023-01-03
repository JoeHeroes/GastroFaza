﻿using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class UpdateWorkerDto
    {
        [EmailAddress]
        public string Email { get; set; }

        public float Salary { get; set; }

        public int Rating { get; set; }

        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
    }
}