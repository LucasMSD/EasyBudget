using EasyBudget.Data.Dto.CategoryDto;
using EasyBudget.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace EasyBudget.Documentations.SwaggerExamples
{
    public class CreateCategoryRequestExample : IExamplesProvider<CreateCategoryDto>
    {
        public CreateCategoryDto GetExamples()
        {
            return new CreateCategoryDto()
            {
                Name = "Hobbies",
                Type = FinancialType.Expense
            };
        }
    }
}
