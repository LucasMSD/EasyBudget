using EasyBudget.Enums;

namespace EasyBudget.Data.Dto.CategoryDto
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public FinancialType Type { get; set; }
    }
}
