using System.ComponentModel.DataAnnotations;

namespace EasyBudget.Attributes.Validations
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PastDateOnlyAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            DateTime date = Convert.ToDateTime(value);

            return date.Date <= DateTime.Now.Date;
        }
    }
}
