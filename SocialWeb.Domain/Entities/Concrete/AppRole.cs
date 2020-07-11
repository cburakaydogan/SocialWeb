using SocialWeb.Domain.Entities.Abstract;
using SocialWeb.Domain.Enums;
using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text;

namespace SocialWeb.Domain.Entities.Concrete
{
    public class AppRole : IdentityRole<int>, IBaseEntity
    {
        public DateTime CreateDate { get { return DateTime.Now; } private set { } }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get; set; }
    }
}
