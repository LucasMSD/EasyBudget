using EasyBudget.Data.Dto.MovementDto;
using Swashbuckle.AspNetCore.Filters;

namespace EasyBudget.Documentations.SwaggerExamples
{
    public class CreateMovementRequestExample : IExamplesProvider<CreateMovementDto>
    {
        public CreateMovementDto GetExamples()
        {
            return new CreateMovementDto()
            {
                Amount = 20.10m,
                Title = "A carton of 20 eggs",
                Date = new DateTime(2023, 5, 7),
                CategoryId = 3,
                Type = Enums.FinancialType.Expense,
                Description = "20 eggs bought at the market near work"
            };
        }
    }
}
