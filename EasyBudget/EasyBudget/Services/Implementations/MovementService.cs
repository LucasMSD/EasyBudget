using EasyBudget.Data.Dto.MovementDto;
using EasyBudget.Services.IServices;
using FluentResults;

namespace EasyBudget.Services.Implementations
{
    public class MovementService : IMovementService
    {
        public Task<Result<ReadMovementDto>> CreateAsync(CreateMovementDto createMovementDto)
        {
            throw new NotImplementedException();
        }

        public Task<Result> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<ReadMovementDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Result<ReadMovementDto>> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateAsync(UpdateMovementDto movement)
        {
            throw new NotImplementedException();
        }
    }
}
