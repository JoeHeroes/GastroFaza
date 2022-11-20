﻿using System.ComponentModel.DataAnnotations;

namespace GastroFaza.Models.DTO
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
