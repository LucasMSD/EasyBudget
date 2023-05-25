using Dapper;
using EasyBudget.Data.Models;
using EasyBudget.Enums;
using EasyBudget.Repositories.IRepositories;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

namespace EasyBudget.Repositories.Implementations
{
    public class MovementRepository : IMovementRepository
    {
        private readonly SqlConnection _conn;

        public MovementRepository(SqlConnection conn)
        {
            _conn = conn;
        }

        public async Task DeleteAsync(long id)
        {
            string command = @"
DELETE FROM Movement
WHERE id = @id
";

            await _conn.ExecuteAsync(command, new { id });
        }

        public async Task<IEnumerable<Movement>> FindAllAsync()
        {
            string query = @"
SELECT
    *
FROM Movement;";

            return await _conn.QueryAsync<Movement>(query);
        }

        public async Task<IEnumerable<Movement>> FindAllByCategoryAsync(long categoryId)
        {
            string query = @"
SELECT
    *
FROM Movement
WHERE category_id = @category_id;";

            return await _conn.QueryAsync<Movement>(query, new { category_id = categoryId });
        }

        public async Task<Movement?> FindByIdAsync(long id)
        {
            string query = @"
SELECT
    *
FROM Movement
WHERE id = @id;";

            return await _conn.QueryFirstOrDefaultAsync<Movement?>(query, new { id });
        }

        public async Task<Movement> InsertAsync(Movement movement)
        {
            string insert = @"
INSERT INTO Movement
(amount, title, date, type, category_id, description)
OUTPUT INSERTED.* VALUES
@amount, @title, @date, @type, @category_id, @description;
";

            var parameters = new DynamicParameters();

            parameters.Add("@amount", movement.Amount);
            parameters.Add("@title", movement.Title);
            parameters.Add("@date", movement.Date);
            parameters.Add("@type", movement.Type);
            parameters.Add("@category_id", movement.CategoryId);
            parameters.Add("@description", movement.Description);

            return await _conn.QuerySingleAsync<Movement>(insert, parameters);
        }

        public async Task<decimal> SumAllMovementsAmount()
        {
            string query = $@"
SELECT
    COUNT(
        CASE
            WHEN type = @type
                THEN amount * (-1)
            ELSE amount
    )
FROM Movement;
";
            return decimal.Round(await _conn.QueryFirstAsync<decimal>(query, new { type = FinancialType.Expense }), 2);
        }

        public async Task UpdateAsync(Movement movement)
        {
            string update = @"
UPDATE Movement
    SET
        amount = @amount,
        title = @title,
        date = @date,
        type = @type,
        category_id = @category_id,
        description = @description
WHERE id = @id
";

            var parameters = new DynamicParameters();

            parameters.Add("@amount", movement.Amount);
            parameters.Add("@title", movement.Title);
            parameters.Add("@date", movement.Date);
            parameters.Add("@type", movement.Type);
            parameters.Add("@category_id", movement.CategoryId);
            parameters.Add("@description", movement.Description);
            parameters.Add("@id", movement.Id);

            await _conn.ExecuteAsync(update, parameters);
        }

        public Task UpdateRangeAsync(IEnumerable<Movement> movements)
        {
            throw new NotImplementedException();
        }
    }
}
