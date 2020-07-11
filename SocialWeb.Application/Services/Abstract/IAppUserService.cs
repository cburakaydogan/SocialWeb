using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using SocialWeb.Application.Models.DTOs;
using System.Threading.Tasks;

namespace SocialWeb.Application.Services.Abstract
{
    public interface IAppUserService
    {
        void DeleteUser(params object[] parameters);
        Task<IdentityResult> Register(RegisterDto model);
        Task<SignInResult> Login(LoginDto model);
        Task LogOut();
        AuthenticationProperties ExternalLogin(string provider, string redirectUrl);
        Task<ExternalLoginInfo> GetExternalLoginInfo();
        Task<SignInResult> ExternalLoginSignIn(string provider, string key);
        Task<IdentityResult> ExternalRegister(ExternalLoginInfo info,ExternalLoginDto model);
    }
}