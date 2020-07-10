using AutoMapper;
using SocialWeb.Application.Services.Abstract;
using SocialWeb.Domain.UnitOfWork;

namespace SocialWeb.Application.Services.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public void DeleteUser(params object[] parameters)
        {
            _unitOfWork.ExecuteSqlRaw("spDeleteUsers {0}", parameters);
        }
    }
}