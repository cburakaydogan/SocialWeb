using SocialWeb.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SocialWeb.Application.Services.Abstract
{
    public interface ILikeService
    {
        Task Like(LikeDto model);
        Task Unlike(LikeDto model);
    }
}
