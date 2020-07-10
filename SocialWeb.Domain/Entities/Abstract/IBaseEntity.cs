using System;
using SocialWeb.Domain.Enums;

namespace SocialWeb.Domain.Entities.Abstract
{
    public interface IBaseEntity
    {
        DateTime CreateDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        DateTime? DeletedDate { get; set; }
        Status Status { get; set; }
    }
}