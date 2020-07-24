using System.ComponentModel.DataAnnotations;

namespace SocialWeb.Application.Models.DTOs
{
    public class LoginDto
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }
        public string Password { get; set; }
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
