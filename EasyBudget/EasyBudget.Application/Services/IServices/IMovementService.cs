using EasyBudget.Data.Dto;
using EasyBudget.Data.Dto.MovementDto;
using FluentResults;

namespace EasyBudget.Services.IServices
{
    public interface IMovementService
    {
        Task<Result<IEnumerable<ReadMovementDto>>> GetAllAsync(int userId, QueryFiltersDto queryFilters);
        Task<Result<ReadMovementDto>> GetByIdAsync(int id, int userId);
        Task<Result<ReadMovementDto>> CreateAsync(CreateMovementDto createMovementDto);
        Task<Result> UpdateAsync(UpdateMovementDto movement);
        Task<Result> DeleteAsync(int id, int userId);
        Task<Result<ReadBalanceDto>> GetBalanceAsync(int userId);
    }
}
