using System.Threading.Tasks;
using AutoMapper;
using SocialWeb.Application.Models.DTOs;
using SocialWeb.Application.Services.Abstract;
using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Domain.UnitOfWork;

namespace SocialWeb.Application.Services.Concrete
{
    public class MentionService : IMentionService
    {
        private IMapper _mapper { get; set; }
        private IUnitOfWork _unitOfWork { get; set; }
        public MentionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddMention(AddMentionDto model)
        {
            var mention = _mapper.Map<AddMentionDto, Mention>(model);

            await _unitOfWork.Mention.Add(mention);
            await _unitOfWork.Commit();
        }
    }
}