using System;
using SocialWeb.Domain.Entities.Abstract;
using SocialWeb.Domain.Enums;

namespace SocialWeb.Domain.Entities.Concrete
{
    public class Mention : IBaseEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public int TweetId { get; set; }
        public Tweet Tweet { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get; set; }
    }
}