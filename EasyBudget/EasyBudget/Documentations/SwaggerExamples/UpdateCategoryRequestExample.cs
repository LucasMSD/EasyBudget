using EasyBudget.Data.Dto.CategoryDto;
using EasyBudget.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace EasyBudget.Documentations.SwaggerExamples
{
    public class UpdateCategoryRequestExample : IExamplesProvider<UpdateCategoryDto>
    {
        public UpdateCategoryDto GetExamples()
        {
            return new UpdateCategoryDto()
            {
                Id = 1,
                Name = "Sports",
                Type = FinancialType.Expense
            };
        }
    }
}
