using EasyBudget.Errors.IErros;
using FluentResults;

namespace EasyBudget.Errors
{
    public class EmailAlreadyExistsError : Error, IBadRequestError
    {
        public EmailAlreadyExistsError() : base("Email already being used.") { }
    }
}
