using EasyBudget.Data.Dto.CategoryDto;
using FluentValidation;

namespace EasyBudget.Data.Dto.Validators.Category
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Type).NotEmpty().IsInEnum();
        }
    }
}
