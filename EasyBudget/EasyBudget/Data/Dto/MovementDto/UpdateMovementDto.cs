using EasyBudget.Enums;
using System.ComponentModel.DataAnnotations;

namespace EasyBudget.Data.Dto.MovementDto
{
    public class UpdateMovementDto : IBaseDto
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }
        [Required]
        public DateOnly Date { get; set; }
        [Required]
        [EnumDataType(typeof(FinancialType))]
        public FinancialType Type { get; set; }
        [Required]
        public long CategoryId { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
