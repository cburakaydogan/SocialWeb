namespace SocialWeb.Application.Models.DTOs
{
    public class LikeDto
    {
        public int AppUserId { get; set; }
        public int TweetId { get; set; }
        public bool isExist { get; set; }
    }
}
