using EasyBudget.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyBudget.Data.Models
{
    [Table("Category")]
    public class Category
    {
        [Column("id")]
        public long Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("type")]
        public FinancialType Type { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }
        [Column("updated")]
        public DateTime Updated { get; set; }
    }
}
