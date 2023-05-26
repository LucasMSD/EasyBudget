using EasyBudget.Data.Dto.CategoryDto;
using FluentResults;

namespace EasyBudget.Services.IServices
{
    public interface ICategoryService
    {
        Task<Result> DeleteAsync(int id, ReplaceCategoryDto replaceCategoryDto);
        Task<Result<IEnumerable<ReadCategoryDto>>> GetAllAsync();
        Task<Result<ReadCategoryDto>> GetByIdAsync(int id);
        Task<Result<ReadCategoryDto>> CreateAsync(CreateCategoryDto createMovementDto);
        Task<Result> UpdateAsync(UpdateCategoryDto movement);
    }
}
