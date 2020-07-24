using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialWeb.Application.Services.Abstract;

namespace SocialWeb.Web.Controllers
{   
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class SearchController : Controller
    {
        private readonly IAppUserService _userService;
        public SearchController(IAppUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index(string keyword)
        {
            ViewBag.SearchKeyword = keyword;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchUser(string keyword, int pageIndex)
        {
            if (!String.IsNullOrEmpty(keyword))
            {
                var users = await _userService.SearchUser(keyword, pageIndex);
                
                return Json(users, new JsonSerializerSettings());
            }
            else
            {
                return NotFound();
            }

        }

    }
}