using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialWeb.Application.Models.DTOs;
using SocialWeb.Application.Services.Abstract;

namespace SocialWeb.Web.Controllers
{
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
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            model.AppUserId = Convert.ToInt32(claim.Value);
            await _mentionService.AddMention(model);
            return Json("Success");
        }
        
    }
}