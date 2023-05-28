using Dapper;
using EasyBudget.Data.Dto.CategoryDto;
using EasyBudget.Enums;
using EasyBudget.Repositories.IRepositories;
using System.Data.SqlClient;

namespace EasyBudget.Repositories.Implementations
{
    public class CategoryRepository :ICategoryRepository
    {
        private readonly SqlConnection _conn;

        public CategoryRepository(SqlConnection conn)
        {
            _conn = conn;
        }

        public async Task DeleteAsync(int id, int userId)
        {
            string command = @"
DELETE FROM Category
WHERE id = @Id
AND user_id = @UserId
";

            await _conn.ExecuteAsync(command, new { Id = id, UserId = userId });
        }

        public async Task<bool> ExistsByIdAsync(int id, int userId)
        {
            string command = @"
SELECT
    CASE
        WHEN EXISTS(
            SELECT 1 FROM Category WHERE id = @Id AND user_id = @UserId)
        THEN 1
        ELSE 0
    END;
";

            return Convert.ToBoolean(await _conn.ExecuteScalarAsync<int>(command, new { Id = id, UserId = userId }));
        }

        public async Task<bool> ExistsByNameAndTypeAsync(string name, FinancialType type, int userId)
        {
            string command = @"
SELECT
    CASE
        WHEN EXISTS(
            SELECT 1 FROM Category WHERE name = @Name AND type = @Type AND user_id = @UserId)
        THEN 1
        ELSE 0
    END;
";

            return Convert.ToBoolean(await _conn.ExecuteScalarAsync<int>(command, new { Name = name, Type = type, UserId = userId }));
        }

        public async Task<IEnumerable<ReadCategoryDto>> FindAllAsync(int userId)
        {
            string query = @$"
SELECT
    id as Id,
    name as Name,
    type as Type,
    CASE
        WHEN type = {(int)FinancialType.Income}
            THEN '{FinancialType.Income}'
        ELSE '{FinancialType.Expense}'
    END as TypeName
FROM Category
WHERE user_id = @UserId";

            return await _conn.QueryAsync<ReadCategoryDto>(query, new { UserId = userId });
        }

        public async Task<ReadCategoryDto?> FindByIdAsync(int id, int userId)
        {
            string query = @$"
SELECT
    id as Id,
    name as Name,
    type as Type,
    CASE
        WHEN type = {(int)FinancialType.Income}
            THEN '{FinancialType.Income}'
        ELSE '{FinancialType.Expense}'
    END as TypeName
FROM Category
WHERE id = @Id
AND user_id = @UserId;";

            return await _conn.QueryFirstOrDefaultAsync<ReadCategoryDto?>(query, new { Id = id, UserId = userId });
        }

        public async Task<ReadCategoryDto> InsertAsync(CreateCategoryDto category)
        {
            string insert = @$"
INSERT INTO Category
(name, type, user_id)
OUTPUT
    INSERTED.id as Id,
    INSERTED.name as Name,
    INSERTED.type as Type,
    CASE
        WHEN INSERTED.type = {(int)FinancialType.Income}
            THEN '{FinancialType.Income}'
        ELSE '{FinancialType.Expense}'
    END as TypeName
VALUES
(@Name, @Type, @UserId) ;
";

            return await _conn.QuerySingleAsync<ReadCategoryDto>(insert, category);
        }

        public async Task UpdateAsync(UpdateCategoryDto category)
        {
            string update = @"
UPDATE Category
    SET
        name = @Name,
        type = @Type
WHERE id = @Id
AND user_id = @UserId
";
            await _conn.ExecuteAsync(update, category);
        }
    }
}
