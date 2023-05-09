using EasyBudget.Data.Dto.CategoryDto;
using FluentValidation;

namespace EasyBudget.Data.Dto.Validators.Category
{
    public class ReplaceCategoryDtoValidator : AbstractValidator<ReplaceCategoryDto>
    {
        public ReplaceCategoryDtoValidator()
        {
            RuleFor(x => x.ReplaceCategoryId).NotEmpty().GreaterThan(0);
        }
    }
}
