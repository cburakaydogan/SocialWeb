
namespace SocialWeb.Application.Models.DTOs
{
    public class FollowDto 
    {
        public int FollowerId { get; set; }
        public int FollowingId { get; set; }
        public bool isExist { get; set; }
    }
}
