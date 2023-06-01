using FluentValidation;

namespace EasyBudget.Data.Dto.UserDto.Validators
{
    public class UserSigninDtoValidator : AbstractValidator<UserSigninDto>
    {
        public UserSigninDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().MaximumLength(320).EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MaximumLength(50);
        }
    }
}
