using System;
using System.ComponentModel.DataAnnotations.Schema;
using SocialWeb.Domain.Entities.Abstract;
using SocialWeb.Domain.Enums;

namespace SocialWeb.Domain.Entities.Concrete
{
    public class Follow : IBaseEntity
    {
        public int FollowerId { get; set; }
        public AppUser Follower { get; set; }
        public int FollowingId { get; set; }
        public AppUser Following { get; set; }
        public DateTime CreateDate { get { return DateTime.Now; } private set { } }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get; set; }
    }
}