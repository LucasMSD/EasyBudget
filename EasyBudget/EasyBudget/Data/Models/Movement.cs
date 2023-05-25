using EasyBudget.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyBudget.Data.Models
{
    [Table("Movement")]
    public class Movement
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("amount")]
        public decimal Amount { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("date")]
        public DateTime Date { get; set; }
        [Column("Type")]
        public FinancialType Type { get; set; }
        [Column("category_id")]
        public long CategoryId { get; set; }
        [Column("description")]
        public string? Description { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }
        [Column("updated")]
        public DateTime Updated { get; set; }
    }
}
