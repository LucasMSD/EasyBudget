using EasyBudget.Data;
using EasyBudget.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EasyBudget.Repositories.Implementations
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task<List<TEntity>> FindAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity?> FindByIdAsync(long id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(long id)
        {
            var entity = await FindByIdAsync(id);
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
