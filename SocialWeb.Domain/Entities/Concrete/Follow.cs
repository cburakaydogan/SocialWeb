using System;
using System.ComponentModel.DataAnnotations.Schema;
using SocialWeb.Domain.Entities.Abstract;
using SocialWeb.Domain.Enums;

namespace SocialWeb.Domain.Entities.Concrete
{
    public class Follow : IBaseEntity
    {
        [ForeignKey("Follower")]
        public Guid FollowerId { get; set; }
        public User Follower { get; set; }

        [ForeignKey("Following")]
        public Guid FollowingId { get; set; }
        public User Following { get; set; }
        public DateTime CreateDate { get{ return DateTime.Now;} }
        public DateTime ModifiedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public Status Status { get; set; }
    }
}