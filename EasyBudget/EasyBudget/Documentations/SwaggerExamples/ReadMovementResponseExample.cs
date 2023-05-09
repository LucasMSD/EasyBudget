using EasyBudget.Data.Dto.MovementDto;
using EasyBudget.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace EasyBudget.Documentations.SwaggerExamples
{
    public class ReadMovementResponseExample : IExamplesProvider<ReadMovementDto>
    {
        public ReadMovementDto GetExamples()
        {
            return new ReadMovementDto()
            {
                Id = 3,
                Amount = 20.1m,
                Title = "A carton of 12 eggs",
                Date = DateOnly.FromDateTime(new DateTime(2023, 5, 7)),
                Category = new Data.Dto.CategoryDto.ReadCategoryDto()
                {
                    Id = 3,
                    Name = "Groceries",
                    Type = FinancialType.Expense
                },
                Type = FinancialType.Expense,
                Description = "20 eggs bought at the market near work"
            };
        }
    }

    public class ListReadMovementResponseExample : IExamplesProvider<List<ReadMovementDto>>
    {
        public List<ReadMovementDto> GetExamples()
        {
            return new List<ReadMovementDto>()
            {
                new ReadMovementDto()
                {
                    Id = 3,
                    Amount = 20.1m,
                    Title = "A carton of 12 eggs",
                    Date = DateOnly.FromDateTime(new DateTime(2023, 5, 7)),
                    Category = new Data.Dto.CategoryDto.ReadCategoryDto()
                    {
                        Id = 3,
                        Name = "Groceries",
                        Type = FinancialType.Expense
                    },
                    Type = FinancialType.Expense,
                    Description = "20 eggs bought at the market near work"
                },
                new ReadMovementDto()
                {
                    Id = 4,
                    Amount = 100.5m,
                    Title = "Dinner with Leyse",
                    Date = DateOnly.FromDateTime(new DateTime(2023, 5, 8)),
                    Category = new Data.Dto.CategoryDto.ReadCategoryDto()
                    {
                        Id = 3,
                        Name = "Restaurants",
                        Type = FinancialType.Expense
                    },
                    Type = FinancialType.Expense,
                },
            };
        }
    }
}
