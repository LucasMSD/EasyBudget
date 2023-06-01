using FluentValidation;

namespace EasyBudget.Data.Dto.UserDto.Validators
{
    public class UserSignupDtoValidator : AbstractValidator<UserSignupDto>
    {
        public UserSignupDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(20);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().MaximumLength(320).EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MaximumLength(50);
            RuleFor(x => x.ConfirmPassword).NotEmpty().MaximumLength(50).Equal(x => x.Password).WithMessage($"'Confirm Password' must be equal to {nameof(UserSigninDto.Password)}.");
            RuleFor(x => x.Birth).NotEmpty();
        }
    }
}
