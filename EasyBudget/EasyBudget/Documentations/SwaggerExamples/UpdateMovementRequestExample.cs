using EasyBudget.Data.Dto.MovementDto;
using Swashbuckle.AspNetCore.Filters;

namespace EasyBudget.Documentations.SwaggerExamples
{
    public class UpdateMovementRequestExample : IExamplesProvider<UpdateMovementDto>
    {
        public UpdateMovementDto GetExamples()
        {
            return new UpdateMovementDto()
            {
                Id = 3,
                Amount = 20.10m,
                Title = "A carton of 12 eggs",
                Date = new DateTime(2023, 5, 7),
                CategoryId = 3,
                Type = Enums.FinancialType.Expense,
                Description = "12 eggs bought at the market near home"
            };
        }
    }
}
