using System;

namespace SocialWeb.Application.Models.DTOs
{
    public class AddMentionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int AppUserId { get; set; }
        public int TweetId { get; set; }
        public DateTime CreateDate { get; set; }
    }
}