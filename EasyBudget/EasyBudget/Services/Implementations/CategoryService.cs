using AutoMapper;
using EasyBudget.Data.Dto.CategoryDto;
using EasyBudget.Data.Models;
using EasyBudget.Errors;
using EasyBudget.Repositories.IRepositories;
using EasyBudget.Services.IServices;
using FluentResults;
using System.Collections;

namespace EasyBudget.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMovementRepository _movementRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMovementRepository movementRepository,IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _movementRepository = movementRepository;
        }

        public async Task<Result<IEnumerable<ReadCategoryDto>>> GetAllAsync()
            => Result.Ok(_mapper.Map<IEnumerable<ReadCategoryDto>>(await _categoryRepository.FindAllAsync()));

        public async Task<Result<ReadCategoryDto>> GetByIdAsync(long id)
        {
            if (id <= 0)
            {
                return Result.Fail(new IdLessThanZeroError());
            }

            var category = await _categoryRepository.FindByIdAsync(id);

            if (category == null)
            {
                return Result.Fail(new CategoryNotFoundError());
            }

            return Result.Ok(_mapper.Map<ReadCategoryDto>(category));
        }

        public async Task<Result<ReadCategoryDto>> CreateAsync(CreateCategoryDto createCategoryDto)
        {
            if (await _categoryRepository.ExistsByNameAndTypeAsync(createCategoryDto.Name, createCategoryDto.Type))
            {
                return Result.Fail(new CategoryAlreadyExistsError());
            }

            var category = _mapper.Map<Category>(createCategoryDto);
            category.Created = DateTime.Now;
            category.Updated = DateTime.Now;

            var insertedCategory = await _categoryRepository.InsertAsync(category);

            return Result.Ok(_mapper.Map<ReadCategoryDto>(insertedCategory));
        }

        public async Task<Result> UpdateAsync(UpdateCategoryDto updateCategoryDto)
        {
            if (await _categoryRepository.ExistsByNameAndTypeAsync(updateCategoryDto.Name, updateCategoryDto.Type))
            {
                return Result.Fail(new CategoryAlreadyExistsError());
            }

            var category = await _categoryRepository.FindByIdAsync(updateCategoryDto.Id);

            if (category == null)
            {
                return Result.Fail(new CategoryNotFoundError());
            }

            _mapper.Map(updateCategoryDto, category);
            category.Updated = DateTime.Now;

            await _categoryRepository.UpdateAsync(category);

            return Result.Ok();
        }

        public async Task<Result> DeleteAsync(long deleteCategoryid, ReplaceCategoryDto deleteCategoryDto)
        {
            var deleteCategory = await _categoryRepository.FindByIdAsync(deleteCategoryid);

            if (deleteCategory == null)
            {
                return Result.Fail(new CategoryNotFoundError());
            }

            var replaceCategory = await _categoryRepository.FindByIdAsync(deleteCategoryDto.ReplaceCategoryId);

            if (replaceCategory == null)
            {
                return Result.Fail(new CategoryNotFoundError().WithMetadata("Field", nameof(deleteCategoryDto.ReplaceCategoryId)));
            }

            var movements = await _movementRepository.FindAllByCategoryAsync(deleteCategoryid);

            if (movements.Any())
            {
                foreach (var movement in movements)
                {
                    movement.CategoryId = deleteCategoryDto.ReplaceCategoryId;
                }
            }

            await _movementRepository.UpdateRangeAsync(movements);
            await _categoryRepository.DeleteAsync(deleteCategoryid);

            return Result.Ok();
        }

        public Task<Result> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
