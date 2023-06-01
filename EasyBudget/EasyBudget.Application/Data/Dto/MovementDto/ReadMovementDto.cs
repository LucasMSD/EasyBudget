using EasyBudget.Data.Dto.CategoryDto;
using EasyBudget.Enums;

namespace EasyBudget.Data.Dto.MovementDto
{
    public class ReadMovementDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public FinancialType Type { get; set; }
        public string TypeName { get; set; }
        public ReadCategoryDto Category { get; set; }
        public string Description { get; set; }
    }
}
