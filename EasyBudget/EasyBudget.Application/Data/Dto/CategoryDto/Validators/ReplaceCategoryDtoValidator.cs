using FluentValidation;

namespace EasyBudget.Data.Dto.CategoryDto.Validators
{
    public class ReplaceCategoryDtoValidator : AbstractValidator<ReplaceCategoryDto>
    {
        public ReplaceCategoryDtoValidator()
        {
            RuleFor(x => x.ReplaceCategoryId).NotEmpty().GreaterThan(0);
        }
    }
}
