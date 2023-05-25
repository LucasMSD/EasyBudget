using EasyBudget.Data.Models;

namespace EasyBudget.Repositories.IRepositories
{
    public interface IMovementRepository
    {
        Task DeleteAsync(long id);
        Task<IEnumerable<Movement>> FindAllAsync();
        Task<IEnumerable<Movement>> FindAllByCategoryAsync(long categoryId);
        Task<Movement?> FindByIdAsync(long id);
        Task<Movement> InsertAsync(Movement movement);
        Task<decimal> SumAllMovementsAmount();
        Task UpdateAsync(Movement movement);
        Task UpdateRangeAsync(IEnumerable<Movement> movements);
    }
}
