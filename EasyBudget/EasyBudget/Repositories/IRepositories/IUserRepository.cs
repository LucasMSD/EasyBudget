using EasyBudget.Data.Dto.UserDto;
using EasyBudget.Data.Models;

namespace EasyBudget.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<User> FindByCredentials(UserSigninDto userSigninDto);
        Task<bool> AlreadyExistsEmail(string email);
        Task<ReadUserSignupDto> Insert(UserSignupDto userSignupDto);
    }
}
