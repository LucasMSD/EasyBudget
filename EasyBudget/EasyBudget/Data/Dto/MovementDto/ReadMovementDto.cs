using EasyBudget.Data.Dto.CategoryDto;
using EasyBudget.Enums;

namespace EasyBudget.Data.Dto.MovementDto
{
    public class ReadMovementDto : IBaseDto
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public string Title { get; set; }
        public DateOnly Date { get; set; }
        public FinancialType Type { get; set; }
        public ReadCategoryDto Category { get; set; }
        public string Description { get; set; }
    }
}
