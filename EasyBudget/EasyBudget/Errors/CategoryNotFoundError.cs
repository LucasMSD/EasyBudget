using FluentResults;

namespace EasyBudget.Errors
{
    public class CategoryNotFoundError : Error, IBadRequestError
    {
        public CategoryNotFoundError() : base("Category not found.") { }
    }
}
