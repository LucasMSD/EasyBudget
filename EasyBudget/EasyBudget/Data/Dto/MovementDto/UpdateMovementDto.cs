using EasyBudget.Enums;

namespace EasyBudget.Data.Dto.MovementDto
{
    public class UpdateMovementDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public FinancialType Type { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
    }
}
