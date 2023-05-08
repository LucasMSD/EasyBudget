using EasyBudget.Attributes.Validations;
using EasyBudget.Enums;
using System.ComponentModel.DataAnnotations;

namespace EasyBudget.Data.Dto.MovementDto
{
    public class UpdateMovementDto : IBaseDto
    {
        [Required]
        [ValidId(ErrorMessage = "The field Id has to be greater than zero.")]
        public long Id { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "The movement date cannot be greater than the current date.")]
        public decimal Amount { get; set; }
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }
        [Required]
        [PastDateOnly(ErrorMessage = "The movement date cannot be greater than the current date.")]
        public DateTime Date { get; set; }
        [Required]
        [EnumDataType(typeof(FinancialType))]
        public FinancialType Type { get; set; }
        [Required]
        [ValidId(ErrorMessage = "The field CategoryId has to be greater than zero.")]
        public long CategoryId { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
