using AutoMapper;
using EasyBudget.Data.Dto.CategoryDto;
using EasyBudget.Data.Models;
using EasyBudget.Repositories.IRepositories;
using EasyBudget.Services.IServices;
using FluentResults;

namespace EasyBudget.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<ReadCategoryDto>>> GetAllAsync()
            => Result.Ok(_mapper.Map<List<ReadCategoryDto>>(await _categoryRepository.FindAllAsync()));

        public async Task<Result<ReadCategoryDto>> GetByIdAsync(long id)
        {
            if (id <= 0)
            {
                return Result.Fail("The field Id has to be greater than zero.");
            }

            var category = await _categoryRepository.FindByIdAsync(id);

            if (category == null)
            {
                return Result.Fail("Category does not exist.");
            }

            return Result.Ok(_mapper.Map<ReadCategoryDto>(category));
        }

        public async Task<Result<ReadCategoryDto>> CreateAsync(CreateCategoryDto createCategoryDto)
        {
            if (await _categoryRepository.ExistsByNameAndTypeAsync(createCategoryDto.Name, createCategoryDto.Type))
            {
                return Result.Fail("A category with this name and this type already exists.");
            }

            var category = _mapper.Map<Category>(createCategoryDto);
            category.Created = DateTime.Now;
            category.Updated = DateTime.Now;

            await _categoryRepository.CreateAsync(category);

            return Result.Ok(_mapper.Map<ReadCategoryDto>(category));
        }

        public async Task<Result> UpdateAsync(UpdateCategoryDto updateCategoryDto)
        {
            if (updateCategoryDto.Id <= 0)
            {
                return Result.Fail("The field Id has to be greater than zero.");
            }

            if (await _categoryRepository.ExistsByNameAndTypeAsync(updateCategoryDto.Name, updateCategoryDto.Type))
            {
                return Result.Fail("A category with this name and this type already exists.");
            }

            var category = await _categoryRepository.FindByIdAsync(updateCategoryDto.Id);

            if (category == null)
            {
                return Result.Fail("The Id provided does not exist.");
            }

            _mapper.Map(updateCategoryDto, category);
            category.Updated = DateTime.Now;

            await _categoryRepository.UpdateAsync(category);

            return Result.Ok();
        }

        public async Task<Result> DeleteAsync(long id)
        {
            if (id <= 0)
            {
                return Result.Fail("The field Id has to be greater than zero.");
            }

            await _categoryRepository.DeleteAsync(id);

            return Result.Ok();
        }
    }
}
