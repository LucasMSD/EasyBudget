using EasyBudget.Data.Dto;
using EasyBudget.Data.Dto.MovementDto;

namespace EasyBudget.Repositories.IRepositories
{
    public interface IMovementRepository
    {
        Task DeleteAsync(int id, int userId);
        Task<IEnumerable<ReadMovementDto>> FindAllAsync(int userId, QueryFiltersDto QueryFiltersDto);
        Task<IEnumerable<ReadMovementDto>> FindAllByCategoryAsync(int categoryId, int userId);
        Task<ReadMovementDto?> FindByIdAsync(int id, int userId);
        Task<ReadMovementDto> InsertAsync(CreateMovementDto movement);
        Task<decimal> SumAllMovementsAmount(int userId);
        Task UpdateAsync(UpdateMovementDto movement);
        Task UpdateRangeAsync(IEnumerable<UpdateMovementDto> movements);
        Task ReplaceCategory(int oldCategoryId, int newCategoryId, int userId);
        Task<bool> ExistsByIdAsync(int id, int userId);
    }
}
