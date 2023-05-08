using EasyBudget.Data.Dto.CategoryDto;

namespace EasyBudget.Services.IServices
{
    public interface ICategoryService : IBaseCrudService<ReadCategoryDto, CreateCategoryDto, UpdateCategoryDto>
    {
    }
}
