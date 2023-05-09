using EasyBudget.Data.Dto.CategoryDto;
using EasyBudget.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace EasyBudget.Documentations.SwaggerExamples
{
    public class ReadCategoryResponseExample : IExamplesProvider<ReadCategoryDto>
    {
        public ReadCategoryDto GetExamples()
        {
            return new ReadCategoryDto()
            {
                Id = 6,
                Name = "Salary",
                Type = FinancialType.Income
            };
        }
    }

    public class ListCategoryResponseExample : IExamplesProvider<List<ReadCategoryDto>>
    {
        public List<ReadCategoryDto> GetExamples()
        {
            return new List<ReadCategoryDto>()
            {
                new ReadCategoryDto()
                {
                    Id = 6,
                    Name = "Salary",
                    Type = FinancialType.Income
                },
                new ReadCategoryDto()
                {
                    Id = 7,
                    Name = "Groceries",
                    Type = FinancialType.Expense
                },
                new ReadCategoryDto()
                {
                    Id = 8,
                    Name = "Uber",
                    Type = FinancialType.Expense
                },
                new ReadCategoryDto()
                {
                    Id = 9,
                    Name = "Investments",
                    Type = FinancialType.Income
                }
            };

        }
    }
}
