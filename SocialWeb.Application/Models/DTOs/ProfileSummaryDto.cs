using System;
using System.Collections.Generic;
using System.Text;

namespace SocialWeb.Application.Models.DTOs
{
    public class ProfileSummaryDto
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public int FollowerCount { get; set; }
        public int FollowingCount { get; set; }
        public int TweetCount { get; set; }
    }
}
