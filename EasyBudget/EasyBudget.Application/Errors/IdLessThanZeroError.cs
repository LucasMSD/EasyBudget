using EasyBudget.Errors.IErros;
using FluentResults;

namespace EasyBudget.Errors
{
    public class IdLessThanZeroError : Error, IBadRequestError
    {
        public IdLessThanZeroError() : base("The Id has to be greater than zero.") { }
    }
}
