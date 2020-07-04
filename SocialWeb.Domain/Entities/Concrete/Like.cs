using System;
using SocialWeb.Domain.Entities.Abstract;
using SocialWeb.Domain.Enums;

namespace SocialWeb.Domain.Entities.Concrete
{
    public class Like : IBaseEntity
    {
        public Guid TweetId { get; set; }
        public Tweet Tweet { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }  
        public DateTime CreateDate { get{ return DateTime.Now;} }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public Status Status { get; set; }
    }
}