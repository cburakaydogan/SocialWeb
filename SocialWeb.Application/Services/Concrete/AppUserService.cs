using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SocialWeb.Application.AutoMapper;
using SocialWeb.Application.Models.DTOs;
using SocialWeb.Application.Services.Abstract;
using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Domain.UnitOfWork;
using SocialWeb.Infrastructure.Context;

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
        public async Task DeleteUser(params object[] parameters)
        {
            await _unitOfWork.ExecuteSqlRaw("spDeleteUsers {0}", parameters);
        }

        public async Task<IdentityResult> Register(RegisterDto model)
        {
            var user = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent : false);
            }

            return result;
        }

        public async Task<SignInResult> Login(LoginDto model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            return result;
        }

        #region ExternalLogin
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
            return await _signInManager.ExternalLoginSignInAsync(provider, key, isPersistent : false, bypassTwoFactor : true);
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
                    await _signInManager.SignInAsync(user, isPersistent : false);
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
                        await _signInManager.SignInAsync(user, isPersistent : false);
                    }
                }
            }
            return result;
        }

        #endregion
        public async Task<EditProfileDto> GetById(int id)
        {
            var user = await _unitOfWork.AppUser.GetById(id);

            return _mapper.Map<EditProfileDto>(user);
        }
        public async Task EditUser(EditProfileDto model)
        {
            var user = await _unitOfWork.AppUser.GetById(model.Id);

            if (user != null)
            {
                if (model.Image != null)
                {
                    using var image = Image.Load(model.Image.OpenReadStream());
                    image.Mutate(x => x.Resize(256, 256));
                    image.Save("wwwroot/images/users/" + user.UserName + ".jpg");
                    model.ImagePath = ("/images/users/" + user.UserName + ".jpg");
                }
                if(model.Password !=null){
                     user.PasswordHash = _userManager.PasswordHasher.HashPassword(user,model.Password);
                }
                var updatedUser = _mapper.Map<EditProfileDto, AppUser>(model,user);
                await _userManager.UpdateAsync(updatedUser);

                await _unitOfWork.Commit();
            }
        }

        public async Task<ProfileSummaryDto> GetByName(string userName)
        {
            var user = await _unitOfWork.AppUser.GetFilteredFirstorDefault(
                selector: y => new ProfileSummaryDto
                {
                    UserName = y.UserName,
                        Name = y.Name,
                        ImagePath = y.ImagePath,
                        TweetsCount = y.Tweets.Count,
                        FollowersCount = y.Followers.Count,
                        FollowingsCount = y.Followings.Count
                },
                predicate : x => x.UserName == userName);

            return user;
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<int> UserIdFromName(string userName)
        {
            var user = await _unitOfWork.AppUser.GetFilteredFirstorDefault(
                selector: x => x.Id,
                predicate: x => x.UserName == userName);

            return user;
        }
        public async Task<List<SearchUserDto>> SearchUser(string keyword, int pageIndex)
        {
            var users = await _unitOfWork.AppUser.GetFilteredList(
                selector: x => new SearchUserDto
                {
                    Id = x.Id,
                        Name = x.Name,
                        UserName = x.UserName,
                        ImagePath = x.ImagePath
                },
                predicate : x => x.UserName.Contains(keyword) || x.Name.Contains(keyword),
                pageIndex : pageIndex,
                pageSize : 10);

            return users;
        }
    }
}