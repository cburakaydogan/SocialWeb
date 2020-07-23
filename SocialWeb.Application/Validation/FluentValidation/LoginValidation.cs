using FluentValidation;
using SocialWeb.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialWeb.Application.Validation.FluentValidation
{
    public class LoginValidation : AbstractValidator<LoginDto>
    {
        public LoginValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Enter a username");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Please enter a password.");
                
        }
    }
}
