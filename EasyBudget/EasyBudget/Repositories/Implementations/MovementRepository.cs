using EasyBudget.Data;
using EasyBudget.Data.Models;
using EasyBudget.Repositories.IRepositories;

namespace EasyBudget.Repositories.Implementations
{
    public class MovementRepository : BaseRepository<Movement>, IMovementRepository
    {
        public MovementRepository(AppDbContext context) : base(context)
        {
        }
    }
}
