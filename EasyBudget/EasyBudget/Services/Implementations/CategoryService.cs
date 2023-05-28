using EasyBudget.Data.Dto.CategoryDto;
using EasyBudget.Errors;
using EasyBudget.Repositories.IRepositories;
using EasyBudget.Services.IServices;
using FluentResults;

namespace EasyBudget.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMovementRepository _movementRepository;

        public CategoryService(ICategoryRepository categoryRepository, IMovementRepository movementRepository)
        {
            _categoryRepository = categoryRepository;
            _movementRepository = movementRepository;
        }

        public async Task<Result<IEnumerable<ReadCategoryDto>>> GetAllAsync(int userId)
            => Result.Ok(await _categoryRepository.FindAllAsync(userId));

        public async Task<Result<ReadCategoryDto>> GetByIdAsync(int id, int userId)
        {
            if (id <= 0)
                return Result.Fail(new IdLessThanZeroError());

            var category = await _categoryRepository.FindByIdAsync(id, userId);

            if (category == null)
                return Result.Fail(new CategoryNotFoundError());

            return Result.Ok(category);
        }

        public async Task<Result<ReadCategoryDto>> CreateAsync(CreateCategoryDto createCategoryDto)
        {
            if (await _categoryRepository.ExistsByNameAndTypeAsync(createCategoryDto.Name, createCategoryDto.Type, createCategoryDto.UserId))
                return Result.Fail(new CategoryAlreadyExistsError());

            return Result.Ok(await _categoryRepository.InsertAsync(createCategoryDto));
        }

        public async Task<Result> UpdateAsync(UpdateCategoryDto updateCategoryDto)
        {
            if (!await _categoryRepository.ExistsByIdAsync(updateCategoryDto.Id, updateCategoryDto.UserId))
                return Result.Fail(new CategoryNotFoundError());

            if (await _categoryRepository.ExistsByNameAndTypeAsync(updateCategoryDto.Name, updateCategoryDto.Type, updateCategoryDto.UserId))
                return Result.Fail(new CategoryAlreadyExistsError());

            await _categoryRepository.UpdateAsync(updateCategoryDto);

            return Result.Ok();
        }

        public async Task<Result> DeleteAsync(int deleteCategoryid, ReplaceCategoryDto deleteCategoryDto, int userId)
        {
            if (!await _categoryRepository.ExistsByIdAsync(deleteCategoryid, userId))
                return Result.Fail(new CategoryNotFoundError());

            if (!await _categoryRepository.ExistsByIdAsync(deleteCategoryDto.ReplaceCategoryId, userId))
                return Result.Fail(new CategoryNotFoundError().WithMetadata("Field", nameof(deleteCategoryDto.ReplaceCategoryId)));

            await _movementRepository.ReplaceCategory(deleteCategoryid, deleteCategoryDto.ReplaceCategoryId, userId);
            await _categoryRepository.DeleteAsync(deleteCategoryid, userId);

            return Result.Ok();
        }
    }
}
