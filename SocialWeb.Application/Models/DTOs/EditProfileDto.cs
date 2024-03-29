﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SocialWeb.Application.Models.DTOs
{
    public class EditProfileDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        
        [Display(Name = "Username")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
    }
}
