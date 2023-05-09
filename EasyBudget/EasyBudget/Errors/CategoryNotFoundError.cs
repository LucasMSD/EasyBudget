using FluentResults;

namespace EasyBudget.Errors
{
    public class CategoryNotFoundError : Error, INotFoundError
    {
        public CategoryNotFoundError() : base("Category not found.") { }
    }
}
