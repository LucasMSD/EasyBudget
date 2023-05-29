using EasyBudget.Enums;

namespace EasyBudget.Data.Dto
{
    public class QueryFiltersDto
    {
        public string? Title { get; set; }
        public DateTime? Date { get; set; }
        public FinancialType? Type { get; set; }
        public int? CategoryId { get; set; }
    }
}
