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

        public async Task<Result<IEnumerable<ReadCategoryDto>>> GetAllAsync()
            => Result.Ok(await _categoryRepository.FindAllAsync());

        public async Task<Result<ReadCategoryDto>> GetByIdAsync(int id)
        {
            if (id <= 0)
                return Result.Fail(new IdLessThanZeroError());

            var category = await _categoryRepository.FindByIdAsync(id);

            if (category == null)
                return Result.Fail(new CategoryNotFoundError());

            return Result.Ok(category);
        }

        public async Task<Result<ReadCategoryDto>> CreateAsync(CreateCategoryDto createCategoryDto)
        {
            if (await _categoryRepository.ExistsByNameAndTypeAsync(createCategoryDto.Name, createCategoryDto.Type))
                return Result.Fail(new CategoryAlreadyExistsError());

            return Result.Ok(await _categoryRepository.InsertAsync(createCategoryDto));
        }

        public async Task<Result> UpdateAsync(UpdateCategoryDto updateCategoryDto)
        {
            if (!await _categoryRepository.ExistsByIdAsync(updateCategoryDto.Id))
                return Result.Fail(new CategoryNotFoundError());

            if (await _categoryRepository.ExistsByNameAndTypeAsync(updateCategoryDto.Name, updateCategoryDto.Type))
                return Result.Fail(new CategoryAlreadyExistsError());

            await _categoryRepository.UpdateAsync(updateCategoryDto);

            return Result.Ok();
        }

        public async Task<Result> DeleteAsync(int deleteCategoryid, ReplaceCategoryDto deleteCategoryDto)
        {
            if (!await _categoryRepository.ExistsByIdAsync(deleteCategoryid))
                return Result.Fail(new CategoryNotFoundError());

            if (!await _categoryRepository.ExistsByIdAsync(deleteCategoryDto.ReplaceCategoryId))
                return Result.Fail(new CategoryNotFoundError().WithMetadata("Field", nameof(deleteCategoryDto.ReplaceCategoryId)));

            await _movementRepository.ReplaceCategory(deleteCategoryid, deleteCategoryDto.ReplaceCategoryId);
            await _categoryRepository.DeleteAsync(deleteCategoryid);

            return Result.Ok();
        }
    }
}
