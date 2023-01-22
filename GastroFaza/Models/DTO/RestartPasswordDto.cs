using Microsoft.Build.Framework;
using System.ComponentModel;

namespace GastroFaza.Models.DTO
{
    public class RestartPasswordDto
    {
        [Required]
        [DisplayName("Old Password")]
        public string OldPassword { get; set; }
        [Required]
        [DisplayName("New Password")]
        public string NewPassword { get; set; }
        [Required]
        [DisplayName("Confirm New Password")]
        public string ConfirmNewPassword { get; set; }

    }
}
