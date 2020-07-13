using System;
using System.Collections.Generic;
using SocialWeb.Domain.Entities.Abstract;
using SocialWeb.Domain.Enums;

namespace SocialWeb.Domain.Entities.Concrete
{
    public class Tweet : IBaseEntity
    {
        public Tweet()
        {
            Likes = new List<Like>();
            Shares = new List<Share>();
            Mentions = new List<Mention>();
        }
        public int Id { get; set; }
        public string Text { get; set; }
        public string ImagePath { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<Like> Likes { get; set; }
        public List<Share> Shares { get; set; }
        public List<Mention> Mentions { get; set; }
        public DateTime CreateDate { get { return DateTime.Now; } private set { } }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get; set; }
    }
}