using EasyBudget.Data.Dto.CategoryDto;
using EasyBudget.Enums;

namespace EasyBudget.Repositories.IRepositories
{
    public interface ICategoryRepository
    {
        Task<bool> ExistsByIdAsync(int id);
        Task<bool> ExistsByNameAndTypeAsync(string name, FinancialType type);
        Task DeleteAsync(int id);
        Task<IEnumerable<ReadCategoryDto>> FindAllAsync();
        Task<ReadCategoryDto?> FindByIdAsync(int id);
        Task<ReadCategoryDto> InsertAsync(CreateCategoryDto category);
        Task UpdateAsync(UpdateCategoryDto category);
    }
}
