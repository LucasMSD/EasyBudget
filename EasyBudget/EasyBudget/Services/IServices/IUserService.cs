using EasyBudget.Data.Dto.UserDto;
using FluentResults;

namespace EasyBudget.Services.IServices
{
    public interface IUserService
    {
        Task<Result<ReadUserSignupDto>> Signup(UserSignupDto userSignupDto);
        Task<Result<TokenLoginDto>> Signin(UserSigninDto userSiginDto);
    }
}
