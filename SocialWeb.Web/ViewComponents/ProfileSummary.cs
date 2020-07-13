using Microsoft.AspNetCore.Mvc;
using SocialWeb.Application.Services.Abstract;
using SocialWeb.Application.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialWeb.Web.ViewComponents
{
    public class ProfileSummary : ViewComponent
    {
        private readonly IAppUserService _userservice;

        public ProfileSummary(IAppUserService service)
        {
            _userservice = service;
        }
        public async Task<IViewComponentResult> InvokeAsync(string userName)
        {
            var user = await _userservice.GetByName(userName);

            return View(user);
        }
    }
}
