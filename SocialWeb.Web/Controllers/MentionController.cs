using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialWeb.Application.Models.DTOs;
using SocialWeb.Application.Services.Abstract;

namespace SocialWeb.Web.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class MentionController : Controller
    {
        private IMentionService _mentionService { get; set; }
        public MentionController(IMentionService mentionService)
        {
            _mentionService = mentionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddMention(AddMentionDto model)
        {   
            model.AppUserId = User.GetUserId();
            await _mentionService.AddMention(model);
            return Json("Success");
        }
        
    }
}