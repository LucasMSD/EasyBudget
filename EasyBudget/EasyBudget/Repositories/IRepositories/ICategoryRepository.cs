using EasyBudget.Data.Models;
using EasyBudget.Enums;

namespace EasyBudget.Repositories.IRepositories
{
    public interface ICategoryRepository
    {
        Task<bool> ExistsByIdAsync(long id);
        Task<bool> ExistsByNameAndTypeAsync(string name, FinancialType type);
        Task DeleteAsync(long id);
        Task<IEnumerable<Category>> FindAllAsync();
        Task<Category?> FindByIdAsync(long id);
        Task<Category> InsertAsync(Category category);
        Task UpdateAsync(Category category);
    }
}
