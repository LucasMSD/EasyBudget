using FluentValidation;

namespace EasyBudget.Data.Dto.CategoryDto.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Type).NotEmpty().IsInEnum();
        }
    }
}
