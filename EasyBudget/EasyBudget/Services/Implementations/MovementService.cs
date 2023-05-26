using EasyBudget.Data.Dto.MovementDto;
using EasyBudget.Errors;
using EasyBudget.Repositories.IRepositories;
using EasyBudget.Services.IServices;
using FluentResults;

namespace EasyBudget.Services.Implementations
{
    public class MovementService : IMovementService
    {
        private readonly IMovementRepository _movementRepository;
        private readonly ICategoryRepository _categoryRepository;

        public MovementService(IMovementRepository movementRepository, ICategoryRepository categoryRepository)
        {
            _movementRepository = movementRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<IEnumerable<ReadMovementDto>>> GetAllAsync()
            => Result.Ok(await _movementRepository.FindAllAsync());

        public async Task<Result<ReadMovementDto>> GetByIdAsync(int id)
        {
            if (id <= 0)
                return Result.Fail(new IdLessThanZeroError());

            var movement = await _movementRepository.FindByIdAsync(id);

            if (movement == null)
                return Result.Fail(new MovementNotFoundError());

            return Result.Ok(movement);
        }

        public async Task<Result<ReadBalanceDto>> GetBalanceAsync()
            => Result.Ok(new ReadBalanceDto() { Balance = await _movementRepository.SumAllMovementsAmount() });

        public async Task<Result<ReadMovementDto>> CreateAsync(CreateMovementDto createMovementDto)
        {
            var category = await _categoryRepository.FindByIdAsync(createMovementDto.CategoryId);

            if (category == null)
                return Result.Fail(new CategoryNotFoundError());

            if (!createMovementDto.Type.Equals(category.Type))
                return Result.Fail(new InvalidCategoryError());

            var createdMovement = await _movementRepository.InsertAsync(createMovementDto);

            return Result.Ok(createdMovement);
        }

        public async Task<Result> UpdateAsync(UpdateMovementDto updatedMovementDto)
        {
            if (!await _movementRepository.ExistsByIdAsync(updatedMovementDto.Id))
                return Result.Fail(new MovementNotFoundError());

            var category = await _categoryRepository.FindByIdAsync(updatedMovementDto.CategoryId);

            if (category == null)
                return Result.Fail(new CategoryNotFoundError());

            if (!updatedMovementDto.Type.Equals(category.Type))
                return Result.Fail(new InvalidCategoryError());

            await _movementRepository.UpdateAsync(updatedMovementDto);

            return Result.Ok();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            if (id <= 0)
                return Result.Fail(new IdLessThanZeroError());

            await _movementRepository.DeleteAsync(id);

            return Result.Ok();
        }
    }
}
