using Dapper;
using EasyBudget.Data.Dto.UserDto;
using EasyBudget.Data.Models;
using EasyBudget.Repositories.IRepositories;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace EasyBudget.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection _conn;

        public UserRepository(SqlConnection conn)
        {
            _conn = conn;
        }

        public async Task<bool> AlreadyExistsEmail(string email)
        {
            string query = @"
SELECT
    CASE
        WHEN EXISTS(
                SELECT 1
                FROM [User]
                WHERE email = @Email)
        THEN 1
        ELSE 0
    END
";

            return Convert.ToBoolean(await _conn.ExecuteScalarAsync(query, new { Email = email }));
        }

        public async Task<User?> FindByCredentials(UserSigninDto userSigninDto)
        {
            string query = @"
SELECT
    id as Id,
    firstName as FirstName,
    lastName as LastName,
    email as Email,
    birth as Birth
FROM [User]
WHERE email = @Email
AND password = @Password
";

            return await _conn.QueryFirstOrDefaultAsync<User>(query, userSigninDto);
        }

        public async Task<ReadUserSignupDto> Insert(UserSignupDto userSignupDto)
        {
            string query = @"
INSERT INTO [User]
(firstName, lastName, email, password, birth) 
OUTPUT
    inserted.id as Id,
    inserted.firstName as FirstName,
    inserted.lastName as LastName,
    inserted.email as Email,
    inserted.birth as Birth
VALUES
(@FirstName, @LastName, @Email, @Password, @Birth)
";

            return await _conn.QueryFirstOrDefaultAsync<ReadUserSignupDto>(query, userSignupDto);
        }
    }
}
