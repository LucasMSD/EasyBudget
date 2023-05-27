using EasyBudget.Data.Dto.UserDto;
using EasyBudget.Errors;
using EasyBudget.Repositories.IRepositories;
using EasyBudget.Services.IServices;
using FluentResults;

namespace EasyBudget.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        public UserService(IUserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<Result<TokenLoginDto>> Signin(UserSigninDto userSiginDto)
        {
            var user = await _userRepository.FindByCredentials(userSiginDto);

            if (user == null)
            {
                return Result.Fail(new InvalidCredentialsError());
            }

            return Result.Ok(new TokenLoginDto { AccessToken = _tokenService.GenerateAccessToken(user.Id, user.FirstName, user.Email) });
        }

        public async Task<Result<ReadUserSignupDto>> Signup(UserSignupDto userSignupDto)
        {
            if (await _userRepository.AlreadyExistsEmail(userSignupDto.Email))
                return Result.Fail(new EmailAlreadyExistsError());

            return Result.Ok(await _userRepository.Insert(userSignupDto));
        }
    }
}
