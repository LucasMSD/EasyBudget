using EasyBudget.Enums;

namespace EasyBudget.Data.Dto.CategoryDto
{
    public class CreateCategoryDto : IBaseDto
    {
        public string Name { get; set; }
        public FinancialType Type { get; set; }
    }
}
