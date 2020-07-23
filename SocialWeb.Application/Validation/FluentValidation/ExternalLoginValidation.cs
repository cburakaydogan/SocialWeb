using FluentValidation;
using SocialWeb.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SocialWeb.Application.Validation.FluentValidation
{
    public class ExternalLoginValidation : AbstractValidator<ExternalLoginDto>
    {
        public ExternalLoginValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Enter a email address.")
                .EmailAddress().WithMessage("Please enter a valid email address.");

            RuleFor(x => x.Name).NotEmpty().WithMessage("Enter a name").MinimumLength(3).MaximumLength(50).WithMessage("Minimum 3, maximum 50 character.");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Enter a username").MinimumLength(3).MaximumLength(50).WithMessage("Minimum 3, maximum 50 character.");
        }
        
    }
}
