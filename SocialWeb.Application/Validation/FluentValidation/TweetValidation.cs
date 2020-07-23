using FluentValidation;
using SocialWeb.Application.Models.DTOs;

namespace SocialWeb.Application.Validation.FluentValidation
{
    public class TweetValidation: AbstractValidator<SendTweetDto>
    {
        public TweetValidation()
        {
            RuleFor(x => x.Text).NotEmpty().WithMessage("Can not be empty.").MaximumLength(280).WithMessage("Less than 280 character.");
        }
    }
}