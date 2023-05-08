using EasyBudget.Enums;
using System.ComponentModel.DataAnnotations;

namespace EasyBudget.Data.Models
{
    public class Movement
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [MaxLength(50)]
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public FinancialType Type { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
