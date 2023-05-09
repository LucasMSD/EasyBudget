using FluentResults;

namespace EasyBudget.Errors
{
    public class CategoryAlreadyExistsError : Error, IBadRequestError
    {
        public CategoryAlreadyExistsError() : base("A category with this name and this type already exists.") { }
    }
}
