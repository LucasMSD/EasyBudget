using EasyBudget.Enums;

namespace EasyBudget.Data.Dto.CategoryDto
{
    public class ReadCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FinancialType Type { get; set; }
        public string TypeName { get; set; }
    }
}
