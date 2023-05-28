using EasyBudget.Data.Dto.CategoryDto;
using FluentResults;

namespace EasyBudget.Services.IServices
{
    public interface ICategoryService
    {
        Task<Result> DeleteAsync(int id, ReplaceCategoryDto replaceCategoryDto, int userId);
        Task<Result<IEnumerable<ReadCategoryDto>>> GetAllAsync(int userId);
        Task<Result<ReadCategoryDto>> GetByIdAsync(int id, int userId);
        Task<Result<ReadCategoryDto>> CreateAsync(CreateCategoryDto createMovementDto);
        Task<Result> UpdateAsync(UpdateCategoryDto movement);
    }
}
