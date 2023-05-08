using EasyBudget.Data.Dto.MovementDto;

namespace EasyBudget.Services.IServices
{
    public interface IMovementService : IBaseCrudService<ReadMovementDto, CreateMovementDto, UpdateMovementDto>
    {
    }
}
