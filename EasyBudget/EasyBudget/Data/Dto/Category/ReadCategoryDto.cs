using EasyBudget.Enums;

namespace EasyBudget.Data.Dto.Category
{
    public class ReadCategoryDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public FinancialType Type { get; set; }
    }
}
