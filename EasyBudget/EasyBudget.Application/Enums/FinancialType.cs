using System.ComponentModel;

namespace EasyBudget.Enums
{
    public enum FinancialType
    {
        [Description("None")]
        None,
        [Description("Income")]
        Income,
        [Description("Expense")]
        Expense
    }
}
