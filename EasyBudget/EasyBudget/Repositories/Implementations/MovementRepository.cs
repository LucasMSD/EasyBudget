using Dapper;
using EasyBudget.Data.Dto.CategoryDto;
using EasyBudget.Data.Dto.MovementDto;
using EasyBudget.Enums;
using EasyBudget.Repositories.IRepositories;
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

        public async Task DeleteAsync(int id)
        {
            string command = @"
DELETE FROM Movement
WHERE id = @Id
";

            await _conn.ExecuteAsync(command, new { Id = id });
        }

        public async Task<IEnumerable<ReadMovementDto>> FindAllAsync()
        {
            string query = @$"
SELECT
    [m].id as Id,
    [m].amount as Amount,
    [m].title as Title,
    [m].date as Date,
    [m].type as Type,
    CASE
        WHEN [m].type = {(int)FinancialType.Income}
            THEN '{FinancialType.Income}'
        ELSE '{FinancialType.Expense}'
    END as TypeName,
    [m].description as Description,
    [c].id as Id,
    [c].name as Name,
    [c].type as Type,
    CASE
        WHEN [c].type = {(int)FinancialType.Income}
            THEN '{FinancialType.Income}'
        ELSE '{FinancialType.Expense}'
    END as TypeName
FROM Movement as [m]
    JOIN Category as [c]
        on [m].category_id = [c].id;";

            return await _conn.QueryAsync<ReadMovementDto, ReadCategoryDto, ReadMovementDto>(query, (movement, category) =>
            {
                movement.Category = category;
                return movement;
            });
        }

        public async Task<IEnumerable<ReadMovementDto>> FindAllByCategoryAsync(int categoryId)
        {
            string query = @$"
SELECT
    [m].id as Id,
    [m].amount as Amount,
    [m].title as Title,
    [m].date as Date,
    [m].type as Type,
    CASE
        WHEN [m].type = {(int)FinancialType.Income}
            THEN '{FinancialType.Income}'
        ELSE '{FinancialType.Expense}'
    END as TypeName,
    [m].description as Description,
    [c].id as Id,
    [c].name as Name,
    [c].type as Type,
    CASE
        WHEN [c].type = {(int)FinancialType.Income}
            THEN '{FinancialType.Income}'
        ELSE '{FinancialType.Expense}'
    END as TypeName
FROM Movement as [m]
    JOIN Category as [c]
        on [m].category_id = [c].id
WHERE [m].category_id = @CategoryId;";

            return await _conn.QueryAsync<ReadMovementDto, ReadCategoryDto, ReadMovementDto>(query, (movement, category) =>
            {
                movement.Category = category;
                return movement;
            }, new { CategoryId = categoryId });
        }

        public async Task<ReadMovementDto?> FindByIdAsync(int id)
        {
            string query = @$"
SELECT
    [m].id as Id,
    [m].amount as Amount,
    [m].title as Title,
    [m].date as Date,
    [m].type as Type,
    CASE
        WHEN [m].type = {(int)FinancialType.Income}
            THEN '{FinancialType.Income}'
        ELSE '{FinancialType.Expense}'
    END as TypeName,
    [m].description as Description,
    [c].id as Id,
    [c].name as Name,
    [c].type as Type,
    CASE
        WHEN [c].type = {(int)FinancialType.Income}
            THEN '{FinancialType.Income}'
        ELSE '{FinancialType.Expense}'
    END as TypeName
FROM Movement as [m]
    JOIN Category as [c]
        on [m].category_id = [c].id
WHERE [m].id = @Id;";

            return (await _conn.QueryAsync<ReadMovementDto, ReadCategoryDto, ReadMovementDto?>(query, (movement, category) =>
            {
                movement.Category = category;
                return movement;
            }, new { Id = id })).FirstOrDefault();
        }

        public async Task<ReadMovementDto> InsertAsync(CreateMovementDto movement)
        {
            string insert = @$"
INSERT INTO Movement
(amount, title, date, type, category_id, description) VALUES
(@Amount, @Title, @Date, @Type, @CategoryId, @Description);

SELECT
    [m].id as Id,
    [m].amount as Amount,
    [m].title as Title,
    [m].date as Date,
    [m].type as Type,
    CASE
        WHEN [m].type = {(int)FinancialType.Income}
            THEN '{FinancialType.Income}'
        ELSE '{FinancialType.Expense}'
    END as TypeName,
    [m].description as Description,
    [c].id as Id,
    [c].name as Name,
    [c].type as Type,
    CASE
        WHEN [c].type = {(int)FinancialType.Income}
            THEN '{FinancialType.Income}'
        ELSE '{FinancialType.Expense}'
    END as TypeName
FROM Movement as [m]
    JOIN Category as [c]
        on [m].category_id = [c].id
WHERE [m].id = SCOPE_IDENTITY();
";

            return (await _conn.QueryAsync<ReadMovementDto, ReadCategoryDto, ReadMovementDto>(insert, map: (movement, category) =>
            {
                movement.Category = category;
                return movement;
            }, param: movement)).First();
        }

        public async Task<decimal> SumAllMovementsAmount()
        {
            string query = $@"
SELECT
    SUM(
        CASE
            WHEN type = {(int)FinancialType.Expense}
                THEN amount * (-1)
            ELSE amount
        END
    )
FROM Movement;
";
            return decimal.Round(await _conn.QueryFirstAsync<decimal>(query), 2);
        }

        public async Task UpdateAsync(UpdateMovementDto movement)
        {
            string update = @"
UPDATE Movement
    SET
        amount = @Amount,
        title = @Title,
        date = @Date,
        type = @Type,
        category_id = @CategoryId,
        description = @Description
WHERE id = @Id
";

            await _conn.ExecuteAsync(update, movement);
        }

        public async Task UpdateRangeAsync(IEnumerable<UpdateMovementDto> movements)
        {
            string update = @"
UPDATE Movement
    SET
        amount = @Amount,
        title = @Title,
        date = @Date,
        type = @Type,
        category_id = @CategoryId,
        description = @Description
WHERE id = @Id
";

            await _conn.ExecuteAsync(update, movements);
        }

        public async Task ReplaceCategory(int oldCategoryId, int newCategoryId)
        {
            string update = @"
UPDATE Movement
    SET category_id = @NewCategoryId
WHERE category_id = @OldCategoryId;
";

            await _conn.ExecuteAsync(update, new { NewCategoryId = newCategoryId, OldCategoryId = oldCategoryId });
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            string query = @"
SELECT
    CASE
        WHEN EXISTS(
            SELECT 1 FROM Movement WHERE id = @Id)
        THEN 1
        ELSE 0
    END;
";

            return Convert.ToBoolean(await _conn.ExecuteScalarAsync<int>(query, new { Id = id }));
        }
    }
}
