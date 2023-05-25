using Dapper;
using EasyBudget.Data.Models;
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

        public async Task DeleteAsync(long id)
        {
            string command = @"
DELETE FROM Category
WHERE id = @id
";

            await _conn.ExecuteAsync(command, new { id });
        }

        public async Task<bool> ExistsByIdAsync(long id)
        {
            string command = @"
SELECT
    CASE
        WHEN EXISTS(
            SELECT * FROM Cliente WHERE id = @id)
        THEN 1
        ELSE 0
    END;
";

            return Convert.ToBoolean(await _conn.ExecuteScalarAsync<int>(command, new {id}));
        }

        public async Task<bool> ExistsByNameAndTypeAsync(string name, FinancialType type)
        {
            string command = @"
SELECT
    CASE
        WHEN EXISTS(
            SELECT * FROM Cliente WHERE name = @name and type = @type)
        THEN 1
        ELSE 0
    END;
";

            return Convert.ToBoolean(await _conn.ExecuteScalarAsync<int>(command, new { @name, @type }));
        }

        public async Task<IEnumerable<Category>> FindAllAsync()
        {
            string query = @"
SELECT
    *
FROM Category;";

            return await _conn.QueryAsync<Category>(query);
        }

        public async Task<Category?> FindByIdAsync(long id)
        {
            string query = @"
SELECT
    *
FROM Category
WHERE id = @id;";

            return await _conn.QueryFirstOrDefaultAsync<Category?>(query, new { id });
        }

        public async Task<Category> InsertAsync(Category category)
        {
            string insert = @"
INSERT INTO Category
(name, type)
OUTPUT INSERTED.* VALUES
@name, @type;
";

            var parameters = new DynamicParameters();

            parameters.Add("@name", category.Name);
            parameters.Add("@type", category.Type);

            return await _conn.QuerySingleAsync<Category>(insert, parameters);
        }

        public async Task UpdateAsync(Category category)
        {
            string update = @"
UPDATE Movement
    SET
        name = @name,
        type = @type,
WHERE id = @id
";

            var parameters = new DynamicParameters();

            parameters.Add("@name", category.Name);
            parameters.Add("@type", category.Type);
            parameters.Add("@id", category.Id);

            await _conn.ExecuteAsync(update, parameters);
        }
    }
}
