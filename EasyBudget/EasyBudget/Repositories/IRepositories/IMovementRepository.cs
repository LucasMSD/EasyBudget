using EasyBudget.Data.Dto.CategoryDto;
using EasyBudget.Data.Models;

namespace EasyBudget.Repositories.IRepositories
{
    public interface IMovementRepository : IBaseRepository<Movement>
    {
        Task<List<Movement>> FindAllByCategoryAsync(long categoryId);
        Task<decimal> SumAllMovementsAmount();
    }
}
