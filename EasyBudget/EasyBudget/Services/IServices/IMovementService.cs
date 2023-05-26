using EasyBudget.Data.Dto.MovementDto;
using FluentResults;

namespace EasyBudget.Services.IServices
{
    public interface IMovementService
    {
        Task<Result<IEnumerable<ReadMovementDto>>> GetAllAsync();
        Task<Result<ReadMovementDto>> GetByIdAsync(int id);
        Task<Result<ReadMovementDto>> CreateAsync(CreateMovementDto createMovementDto);
        Task<Result> UpdateAsync(UpdateMovementDto movement);
        Task<Result> DeleteAsync(int id);
        Task<Result<ReadBalanceDto>> GetBalanceAsync();
    }
}
