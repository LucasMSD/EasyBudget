using EasyBudget.Data.Dto.CategoryDto;
using EasyBudget.Enums;

namespace EasyBudget.Repositories.IRepositories
{
    public interface ICategoryRepository
    {
        Task<bool> ExistsByIdAsync(int id, int userId);
        Task<bool> ExistsByNameAndTypeAsync(string name, FinancialType type, int userId);
        Task DeleteAsync(int id, int userId);
        Task<IEnumerable<ReadCategoryDto>> FindAllAsync(int userId);
        Task<ReadCategoryDto?> FindByIdAsync(int id, int userId);
        Task<ReadCategoryDto> InsertAsync(CreateCategoryDto category);
        Task UpdateAsync(UpdateCategoryDto category);
    }
}
