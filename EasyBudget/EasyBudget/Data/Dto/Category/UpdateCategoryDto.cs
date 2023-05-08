using EasyBudget.Enums;
using System.ComponentModel.DataAnnotations;

namespace EasyBudget.Data.Dto.Category
{
    public class UpdateCategoryDto
    {
        [Required]
        public long Id { get; set; }
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        [Required]
        [EnumDataType(typeof(FinancialType))]
        public FinancialType Type { get; set; }
    }
}
