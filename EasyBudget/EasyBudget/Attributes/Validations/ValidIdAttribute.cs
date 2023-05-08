using System.ComponentModel.DataAnnotations;

namespace EasyBudget.Attributes.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class ValidIdAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            try
            {
                long id = Convert.ToInt64(value);

                return id > 0;
            }
            catch 
            {
                return false;
            }
        }
    }
}
