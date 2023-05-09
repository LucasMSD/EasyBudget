using FluentResults;

namespace EasyBudget.Errors
{
    public class MovementNotFoundError : Error, IBadRequestError
    {
        public MovementNotFoundError() : base("Movement does not exist.") { }
    }
}
