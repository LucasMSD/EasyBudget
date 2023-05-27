using FluentValidation;

namespace EasyBudget.Data.Dto.MovementDto.Validators
{
    public class CreateMovementDtoValidator : AbstractValidator<CreateMovementDto>
    {
        public CreateMovementDtoValidator()
        {
            RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
            RuleFor(x => DateOnly.FromDateTime(x.Date)).NotEmpty().LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now));
            RuleFor(x => x.Type).NotEmpty().IsInEnum();
            RuleFor(x => x.CategoryId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Description).MaximumLength(200);
        }
    }
}
