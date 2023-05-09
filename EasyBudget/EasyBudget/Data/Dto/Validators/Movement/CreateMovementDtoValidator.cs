using EasyBudget.Data.Dto.MovementDto;
using FluentValidation;

namespace EasyBudget.Data.Dto.Validators.Movement
{
    public class CreateMovementDtoValidator : AbstractValidator<CreateMovementDto>
    {
        public CreateMovementDtoValidator()
        {
            RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Date.Date).NotEmpty().LessThanOrEqualTo(DateTime.Now.Date);
            RuleFor(x => x.Type).NotEmpty().IsInEnum();
            RuleFor(x => x.CategoryId).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Description).MaximumLength(200);
        }
    }
}
