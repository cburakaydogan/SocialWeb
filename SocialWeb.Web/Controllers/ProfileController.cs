using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SocialWeb.Web.Controllers
{   [Authorize]
    [AutoValidateAntiforgeryToken]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(string userName)
        {
            ViewBag.userName = userName;
            return View();
        }

    }
}
