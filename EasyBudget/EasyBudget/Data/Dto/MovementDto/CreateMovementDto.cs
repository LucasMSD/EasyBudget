using EasyBudget.Enums;
using System.ComponentModel.DataAnnotations;

namespace EasyBudget.Data.Dto.MovementDto
{
    public class CreateMovementDto : IBaseDto
    {
        [Required]
        public decimal Amount { get; set; }
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public FinancialType Type { get; set; }
        [Required]
        public long CategoryId { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
