using EasyBudget.Enums;
using System.Text.Json.Serialization;

namespace EasyBudget.Data.Dto.CategoryDto
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public FinancialType Type { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
