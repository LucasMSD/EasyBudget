using EasyBudget.Data;
using EasyBudget.Data.Models;
using EasyBudget.Enums;
using EasyBudget.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EasyBudget.Repositories.Implementations
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public async Task<bool> ExistsByIdAsync(long id)
        {
            return await _context.Categories.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> ExistsByNameAndTypeAsync(string name, FinancialType type)
        {
            return await _context.Categories.AnyAsync(x => x.Name.Trim().Equals(name.Trim()) && x.Type.Equals(type));
        }
    }
}
