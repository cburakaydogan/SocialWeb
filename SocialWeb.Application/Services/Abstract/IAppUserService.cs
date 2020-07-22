using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using SocialWeb.Application.Models.DTOs;
using SocialWeb.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialWeb.Application.Services.Abstract
{
    public interface IAppUserService
    {
        Task DeleteUser(params object[] parameters);
        Task<IdentityResult> Register(RegisterDto model);
        Task<SignInResult> Login(LoginDto model);
        Task LogOut();
        Task<int> UserIdFromName(string userName);
        AuthenticationProperties ExternalLogin(string provider, string redirectUrl);
        Task<ExternalLoginInfo> GetExternalLoginInfo();
        Task<SignInResult> ExternalLoginSignIn(string provider, string key);
        Task<IdentityResult> ExternalRegister(ExternalLoginInfo info,ExternalLoginDto model);
        Task<EditProfileDto> GetById(int id);
        Task EditUser(EditProfileDto id);
        Task<ProfileSummaryDto> GetByName(string userName);
        Task<List<SearchUserDto>> SearchUser(string keyword, int pageIndex);

    }
}