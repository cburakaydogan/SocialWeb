using System.Security.Cryptography;
using FluentValidation;
using SocialWeb.Application.Models.DTOs;

namespace SocialWeb.Application.Validation.FluentValidation
{
    public class RegisterValidation : AbstractValidator<RegisterDto>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Enter a email address.")
                .EmailAddress().WithMessage("Please enter a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Please enter a password.");

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords do not match");

            RuleFor(x => x.Name).NotEmpty().MinimumLength(3).MaximumLength(50).WithMessage("Minimum 3, maximum 50 character.");

            RuleFor(x => x.UserName).NotEmpty().MinimumLength(3).MaximumLength(50).WithMessage("Minimum 3, maximum 50 character.");
        }
    }
}