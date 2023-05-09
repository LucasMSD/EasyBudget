using EasyBudget.Data;
using EasyBudget.Data.Models;
using EasyBudget.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EasyBudget.Repositories.Implementations
{
    public class MovementRepository : BaseRepository<Movement>, IMovementRepository
    {
        public MovementRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Movement>> FindAllByCategoryAsync(long categoryId)
        {
            return await _context.Movements.Where(x => x.CategoryId.Equals(categoryId)).ToListAsync();
        }
    }
}
