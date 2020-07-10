using System;
using System.ComponentModel.DataAnnotations;

namespace SocialWeb.Application.Models.DTOs
{
    public class RegisterDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}