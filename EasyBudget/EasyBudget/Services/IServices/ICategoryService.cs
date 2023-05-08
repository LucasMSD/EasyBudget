using EasyBudget.Data.Dto.Category;
using FluentResults;

namespace EasyBudget.Services.IServices
{
    public interface ICategoryService
    {
        Task<Result<List<ReadCategoryDto>>> GetAllAsync();
        Task<Result<ReadCategoryDto>> GetByIdAsync(long id);
        Task<Result<ReadCategoryDto>> CreateAsync(CreateCategoryDto createCategoriaDto);
        Task<Result> UpdateAsync(UpdateCategoryDto category);
        Task<Result> DeleteAsync(long id);
    }
}
