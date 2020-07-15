using SocialWeb.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialWeb.Application.Services.Abstract
{
    public interface ITweetService
    {
        Task<List<TimelineDto>> getTimeline(int userId);
    }
}
