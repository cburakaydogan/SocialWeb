using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using SocialWeb.Application.Models.DTOs;
using SocialWeb.Application.Services.Abstract;
using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Domain.UnitOfWork;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialWeb.Application.Services.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public void DeleteUser(params object[] parameters)
        {
            _unitOfWork.ExecuteSqlRaw("spDeleteUsers {0}", parameters);
        }

        public async Task<IdentityResult> Register(RegisterDto model)
        {
            var user = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }

        public async Task<SignInResult> Login(LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            return result;
        }
        public AuthenticationProperties ExternalLogin(string provider, string redirectUrl)
        {
            return _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        }
        public async Task<ExternalLoginInfo> GetExternalLoginInfo()
        {
            return await _signInManager.GetExternalLoginInfoAsync();
        }

        public async Task<SignInResult> ExternalLoginSignIn(string provider, string key)
        {
            return await _signInManager.ExternalLoginSignInAsync(provider, key, isPersistent: false, bypassTwoFactor: true);
        }

        public async Task<IdentityResult> ExternalRegister(ExternalLoginInfo info, ExternalLoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            IdentityResult result;
            if (user != null)
            {
                result = await _userManager.AddLoginAsync(user, info);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
            }
            else
            {
                model.Principal = info.Principal;
                user = _mapper.Map<AppUser>(model);
                result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                }
            }
            return result;
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}