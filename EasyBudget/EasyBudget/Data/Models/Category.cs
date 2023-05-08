using EasyBudget.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyBudget.Data.Models
{
    public class Category
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }
        [Required]
        public FinancialType Type { get; set; }
        [JsonIgnore]
        public virtual List<Movement> Movements { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Updated { get; set; }
    }
}
