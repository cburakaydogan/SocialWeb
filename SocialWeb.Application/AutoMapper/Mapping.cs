using AutoMapper;
using SocialWeb.Application.Models.DTOs;
using SocialWeb.Domain.Entities.Concrete;

namespace SocialWeb.Application.AutoMapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AppUser, RegisterDto>().ReverseMap();
            CreateMap<AppUser, LoginDto>().ReverseMap();
            CreateMap<AppUser, ExternalLoginDto>().ReverseMap();
        }
    }
}