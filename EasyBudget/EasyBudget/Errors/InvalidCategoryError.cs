using FluentResults;

namespace EasyBudget.Errors
{
    public class InvalidCategoryError : Error, IBadRequestError
    {
        public InvalidCategoryError() : base("The chosen category has a different type (Income or Expense) from the movement type") { }
    }
}
