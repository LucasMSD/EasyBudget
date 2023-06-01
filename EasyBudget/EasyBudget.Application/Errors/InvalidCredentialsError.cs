using EasyBudget.Errors.IErros;
using FluentResults;

namespace EasyBudget.Errors
{
    public class InvalidCredentialsError : Error, IBadRequestError
    {
        public InvalidCredentialsError() : base ("Invalid Credentials.") { }
    }
}
