using EasyBudget.Data.Models;
using EasyBudget.Enums;

namespace EasyBudget.Repositories.IRepositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<bool> ExistsByIdAsync(long id);
        Task<bool> ExistsByNameAndTypeAsync(string name, FinancialType type);
    }
}
