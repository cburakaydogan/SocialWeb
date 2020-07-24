using System.Collections.Generic;

namespace SocialWeb.Application.Models.DTOs
{
    public class FollowListVm
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }   
        public string ImagePath { get; set; }
        public List<int> Follows { get; set; }
    }
}