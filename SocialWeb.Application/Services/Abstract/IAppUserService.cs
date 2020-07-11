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
    }
}