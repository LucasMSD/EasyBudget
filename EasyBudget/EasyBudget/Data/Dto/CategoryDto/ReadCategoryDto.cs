using EasyBudget.Enums;

namespace EasyBudget.Data.Dto.CategoryDto
{
    public class ReadCategoryDto : IBaseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public FinancialType Type { get; set; }
    }
}
