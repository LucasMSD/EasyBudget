using EasyBudget.Data.Dto.MovementDto;
using Swashbuckle.AspNetCore.Filters;

namespace EasyBudget.Documentations.SwaggerExamples
{
    public class ReadBalanceResponseExample : IExamplesProvider<ReadBalanceDto>
    {
        public ReadBalanceDto GetExamples()
        {
            return new ReadBalanceDto()
            {
                Balance = 3247.73m
            };
        }
    }
}
