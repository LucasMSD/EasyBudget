using EasyBudget.Data.Dto.MovementDto;
using FluentResults;

namespace EasyBudget.Services.IServices
{
    public interface IMovementService : IBaseCrudService<ReadMovementDto, CreateMovementDto, UpdateMovementDto>
    {
        Task<Result<ReadBalanceDto>> GetBalanceAsync();
    }
}
