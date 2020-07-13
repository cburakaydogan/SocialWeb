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
using System.Linq;
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
        private readonly ApplicationDbContext _context;

        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

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
                await _signInManager.SignInAsync(user, isPersistent: false);
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

        #endregion
        public async Task<EditProfileDto> GetById (int id)
        {
            var user = await _unitOfWork.AppUser.GetById(id);

            return _mapper.Map<EditProfileDto>(user);
        }
        public async Task EditUser(EditProfileDto model)
        {
            var isUser = await _unitOfWork.AppUser.GetById(model.Id);
            if (isUser != null)
            {
                if (model.Image != null)
                {
                    using var image = Image.Load(model.Image.OpenReadStream());
                    image.Mutate(x => x.Resize(256, 256));
                    model.ImagePath = "wwwroot/images/users/" + isUser.UserName+ ".jpg";
                    image.Save(model.ImagePath);
                }

                var user = _mapper.Map<EditProfileDto,AppUser>(model,isUser);

                _unitOfWork.AppUser.Update(user);
                await _unitOfWork.Commit();
            }
        }

        public async Task<ProfileSummaryDto> GetByName(string userName)
        {
            var user = await _context.AppUsers.Where(x => x.UserName == userName).Select(x => new ProfileSummaryDto()
            {
                Name = x.Name,
                FollowerCount = x.Followers.Count,
                FollowingCount = x.Followings.Count,
                ImagePath = x.ImagePath,
                UserName = x.UserName,
                TweetCount = x.Tweets.Count
            }).FirstAsync();
            //var user = await _context.AppUsers.Where(x => x.UserName == userName).ProjectTo<ProfileSummaryDto>(_mapper.ConfigurationProvider).FirstAsync();

            return user;
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UserDto> GetFromName(string userName)
        {
            var user = await _context.AppUsers.Where(x => x.UserName == userName).Select(x => new UserDto()
            {
                userName = x.UserName,
                Id = x.Id
            }).FirstAsync();
            return user;
        }
    }
}