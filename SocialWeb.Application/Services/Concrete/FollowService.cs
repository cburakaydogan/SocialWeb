using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SocialWeb.Application.Models.DTOs;
using SocialWeb.Application.Services.Abstract;
using SocialWeb.Domain.Entities.Concrete;
using SocialWeb.Domain.UnitOfWork;
using SocialWeb.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialWeb.Application.Services.Concrete
{
    public class FollowService : IFollowService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public FollowService(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
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
            var followingList = await _context.Follows.Where(x => x.FollowerId == id).Select(x=> x.FollowingId).ToListAsync();
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
