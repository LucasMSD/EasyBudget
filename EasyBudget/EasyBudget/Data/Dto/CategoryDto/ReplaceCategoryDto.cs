using EasyBudget.Attributes.Validations;
using System.ComponentModel.DataAnnotations;

namespace EasyBudget.Data.Dto.CategoryDto
{
    public class ReplaceCategoryDto
    {
        [Required]
        [ValidId(ErrorMessage = "The field Id has to be greater than zero.")]
        public long ReplaceCategoryId { get; set; }
    }
}
