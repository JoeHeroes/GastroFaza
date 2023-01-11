using Microsoft.Build.Framework;

namespace GastroFaza.Models.DTO
{
    public class RestartPasswordDto
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmNewPassword { get; set; }

    }
}
