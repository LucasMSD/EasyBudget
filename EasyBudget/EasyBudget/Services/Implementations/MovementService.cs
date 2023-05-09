using AutoMapper;
using EasyBudget.Data.Dto.MovementDto;
using EasyBudget.Data.Models;
using EasyBudget.Repositories.IRepositories;
using EasyBudget.Services.IServices;
using FluentResults;

namespace EasyBudget.Services.Implementations
{
    public class MovementService : IMovementService
    {
        private readonly IMovementRepository _movementRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public MovementService(IMovementRepository movementRepository, ICategoryRepository categoryRepository,IMapper mapper)
        {
            _movementRepository = movementRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<List<ReadMovementDto>>> GetAllAsync()
            => Result.Ok(_mapper.Map<List<ReadMovementDto>>(await _movementRepository.FindAllAsync()));

        public async Task<Result<ReadMovementDto>> GetByIdAsync(long id)
        {
            if (id <= 0)
            {
                return Result.Fail("The field Id has to be greater than zero.");
            }

            var movement = await _movementRepository.FindByIdAsync(id);

            if (movement == null)
            {
                return Result.Fail("Movement does not exist.");
            }

            return Result.Ok(_mapper.Map<ReadMovementDto>(movement));
        }

        public async Task<Result<ReadBalanceDto>> GetBalanceAsync()
            => Result.Ok(new ReadBalanceDto() { Balance = await _movementRepository.SumAllMovementsAmount() });

        public async Task<Result<ReadMovementDto>> CreateAsync(CreateMovementDto createMovementDto)
        {
            var category = await _categoryRepository.FindByIdAsync(createMovementDto.CategoryId);

            if (category == null)
            {
                return Result.Fail("The chosen category does not exist.");
            }

            if (!createMovementDto.Type.Equals(category.Type))
            {
                return Result.Fail("The chosen category has a different type (Income or Expense) from the movement type");
            }

            var movement = _mapper.Map<Movement>(createMovementDto);
            movement.Created = DateTime.Now;
            movement.Updated = DateTime.Now;

            await _movementRepository.CreateAsync(movement);

            return Result.Ok(_mapper.Map<ReadMovementDto>(movement));
        }

        public async Task<Result> UpdateAsync(UpdateMovementDto updatedMovementDto)
        {
            var movement = await _movementRepository.FindByIdAsync(updatedMovementDto.Id);

            if (movement == null)
            {
                return Result.Fail("The Id provided does not exist.");
            }

            var category = await _categoryRepository.FindByIdAsync(updatedMovementDto.CategoryId);

            if (category == null)
            {
                return Result.Fail("The chosen category does not exist.");
            }

            if (!updatedMovementDto.Type.Equals(category.Type))
            {
                return Result.Fail("The chosen category has a different type (Income or Expense) from the movement type");
            }

            _mapper.Map(updatedMovementDto, movement);
            movement.Updated = DateTime.Now;

            await _movementRepository.UpdateAsync(movement);

            return Result.Ok();
        }

        public async Task<Result> DeleteAsync(long id)
        {
            if (id <= 0)
            {
                return Result.Fail("The field Id has to be greater than zero.");
            }

            await _movementRepository.DeleteAsync(id);

            return Result.Ok();
        }
    }
}
