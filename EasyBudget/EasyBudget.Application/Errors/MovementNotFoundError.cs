using EasyBudget.Errors.IErros;
using FluentResults;

namespace EasyBudget.Errors
{
    public class MovementNotFoundError : Error, INotFoundError
    {
        public MovementNotFoundError() : base("Movement does not exist.") { }
    }
}
