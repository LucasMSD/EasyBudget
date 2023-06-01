using EasyBudget.Errors.IErros;
using FluentResults;

namespace EasyBudget.Errors
{
    public class CategoryNotFoundError : Error, INotFoundError
    {
        public CategoryNotFoundError() : base("Category not found.") { }
    }
}
