using EasyBudget.Attributes.Validations;
using EasyBudget.Enums;
using System.ComponentModel.DataAnnotations;

namespace EasyBudget.Data.Dto.CategoryDto
{
    public class UpdateCategoryDto : IBaseDto
    {
        [Required]
        [ValidId(ErrorMessage = "The field Id has to be greater than zero.")]
        public long Id { get; set; }
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required]
        [EnumDataType(typeof(FinancialType))]
        public FinancialType Type { get; set; }
    }
}
