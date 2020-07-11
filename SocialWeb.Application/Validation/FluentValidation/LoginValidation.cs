using FluentValidation;
using SocialWeb.Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialWeb.Application.Validation.FluentValidation
{
    class LoginValidation : AbstractValidator<LoginDto>
    {
        public LoginValidation()
        {
            //RuleFor(x => x.Email).NotEmpty().WithMessage("Enter a email address.")
            //    .EmailAddress().WithMessage("Please enter a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Please enter a password.")
                .MinimumLength(8).MaximumLength(16).Matches("^(?=.*[0-9])(?=.*[a-zA-Z])([a-zA-Z0-9]+)$").WithMessage("Minimum one letter and one number.");
        }
    }
}
