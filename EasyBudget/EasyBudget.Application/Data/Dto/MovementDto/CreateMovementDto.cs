using EasyBudget.Enums;
using System.Text.Json.Serialization;

namespace EasyBudget.Data.Dto.MovementDto
{
    public class CreateMovementDto
    {
        private string? description;

        public decimal Amount { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public FinancialType Type { get; set; }
        public int CategoryId { get; set; }
        public string? Description
        {
            get => description;
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    description = null;
            }
        }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
