using System.Security.Claims;

namespace SocialWeb.Application.Models.DTOs
{
   public class ExternalLoginDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ClaimsPrincipal Principal { get; set; }
    }
}
