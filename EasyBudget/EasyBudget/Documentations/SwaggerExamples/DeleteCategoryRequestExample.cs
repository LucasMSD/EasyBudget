using EasyBudget.Data.Dto.CategoryDto;
using Swashbuckle.AspNetCore.Filters;

namespace EasyBudget.Documentations.SwaggerExamples
{
    public class DeleteCategoryRequestExample : IExamplesProvider<ReplaceCategoryDto>
    {
        public ReplaceCategoryDto GetExamples()
        {
            return new ReplaceCategoryDto() { ReplaceCategoryId = 4 };
        }
    }
}
