using EasyBudget.Enums;

namespace EasyBudget.Data.Dto.CategoryDto
{
    public class UpdateCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FinancialType Type { get; set; }
    }
}
