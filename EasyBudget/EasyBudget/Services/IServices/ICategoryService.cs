using EasyBudget.Data.Dto.CategoryDto;
using FluentResults;

namespace EasyBudget.Services.IServices
{
    public interface ICategoryService : IBaseCrudService<ReadCategoryDto, CreateCategoryDto, UpdateCategoryDto>
    {
        Task<Result> DeleteAsync(long id, ReplaceCategoryDto replaceCategoryDto);
    }
}
