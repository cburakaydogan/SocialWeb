using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SocialWeb.Application.Models.DTOs
{
    public class TweetDetailVm
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ImagePath { get; set; }
        public int AppUserId { get; set; }
        public int LikesCount { get; set; }
        public int MentionsCount { get; set; }
        public int SharesCount { get; set; }
        public DateTime CreateDate { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string UserImage { get; set; }
        public int TimelineOwnerId { get; set; }
        public bool isLiked{get; set;}
        public IEnumerable<MentionDto> Mentions { get; set; }
    }
}