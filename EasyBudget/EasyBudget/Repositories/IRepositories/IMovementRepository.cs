using EasyBudget.Data.Dto.MovementDto;

namespace EasyBudget.Repositories.IRepositories
{
    public interface IMovementRepository
    {
        Task DeleteAsync(int id);
        Task<IEnumerable<ReadMovementDto>> FindAllAsync();
        Task<IEnumerable<ReadMovementDto>> FindAllByCategoryAsync(int categoryId);
        Task<ReadMovementDto?> FindByIdAsync(int id);
        Task<ReadMovementDto> InsertAsync(CreateMovementDto movement);
        Task<decimal> SumAllMovementsAmount();
        Task UpdateAsync(UpdateMovementDto movement);
        Task UpdateRangeAsync(IEnumerable<UpdateMovementDto> movements);
        Task ReplaceCategory(int oldCategoryId, int newCategoryId);
        Task<bool> ExistsByIdAsync(int id);
    }
}
