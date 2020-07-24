using AutoMapper;
using SocialWeb.Application.Models.DTOs;
using SocialWeb.Application.Services.Abstract;
using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Domain.UnitOfWork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialWeb.Application.Services.Concrete
{
    public class FollowService : IFollowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FollowService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Follow(FollowDto model)
        {
            var isExistFollow = await _unitOfWork.Follow.FirstOrDefault(x => x.FollowerId == model.FollowerId && x.FollowingId == model.FollowingId);
            if (isExistFollow == null)
            {
                var follow = _mapper.Map<FollowDto, Follow>(model);
                await _unitOfWork.Follow.Add(follow);
                await _unitOfWork.Commit();
            }
        }

        public async Task<List<int>> FollowingList(int id)
        {
            var followingList = await _unitOfWork.Follow.GetFilteredList(
                 selector: y => y.FollowingId,
                 predicate: x => x.FollowerId == id);

            return followingList;
        }
        public async Task<bool> isFollowing(FollowDto model)
        {
            var isExistFollow = await _unitOfWork.Follow.Any(x => x.FollowerId == model.FollowerId && x.FollowingId == model.FollowingId);

            return isExistFollow;
        }

        public async Task Unfollow(FollowDto model)
        {
            var isExistFollow = await _unitOfWork.Follow.FirstOrDefault(x => x.FollowerId == model.FollowerId && x.FollowingId == model.FollowingId);

            _unitOfWork.Follow.Delete(isExistFollow);
            await _unitOfWork.Commit();
        }
    }
}
