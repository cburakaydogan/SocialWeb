using System.Threading.Tasks;
using SocialWeb.Application.Models.DTOs;

namespace SocialWeb.Application.Services.Abstract
{
    public interface IMentionService
    {
        Task AddMention(AddMentionDto model);
    }
}