namespace EasyBudget.Repositories.IRepositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> FindAllAsync();
        Task<TEntity?> FindByIdAsync(long id);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task DeleteAsync(long id);
    }
}
