using FluentResults;

namespace EasyBudget.Services.IServices
{
    public interface IBaseCrudService<IReadDto, ICreateDro, IUpdateDto>
    {
        Task<Result<List<IReadDto>>> GetAllAsync();
        Task<Result<IReadDto>> GetByIdAsync(long id);
        Task<Result<IReadDto>> CreateAsync(ICreateDro createMovementDto);
        Task<Result> UpdateAsync(IUpdateDto movement);
        Task<Result> DeleteAsync(long id);
    }
}
