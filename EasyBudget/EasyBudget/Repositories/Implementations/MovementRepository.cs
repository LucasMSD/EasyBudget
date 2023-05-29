using Dapper;
using EasyBudget.Data.Dto;
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

        public async Task DeleteAsync(int id, int userId)
        {
            string command = @"
DELETE FROM Movement
WHERE id = @Id
AND user_id = @UserId
";

            await _conn.ExecuteAsync(command, new { Id = id, UserId = userId });
        }

        public async Task<IEnumerable<ReadMovementDto>> FindAllAsync(int userId, QueryFiltersDto queryFiltersDto)
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
WHERE [m].user_id = @UserId
";

            var parameters = new DynamicParameters();

            parameters.Add("UserId", userId);

            if (queryFiltersDto.Title != null)
            {
                query += "AND [m].title like '%@Title%'\n";
                parameters.Add("Title", queryFiltersDto.Title);
            }

            if (queryFiltersDto.Date != null)
            {
                query += "AND [m].date = @Date\n";
                parameters.Add("Date", queryFiltersDto.Date.Value.Date);
            }

            if (queryFiltersDto.Type != null)
            {
                query += "AND [m].type = @Type\n";
                parameters.Add("Type", (int)queryFiltersDto.Type.Value);
            }

            if (queryFiltersDto.CategoryId != null)
            {
                query += "AND [m].category_id = @CategoryId";
                parameters.Add("CategoryId", queryFiltersDto.CategoryId);
            }

            return await _conn.QueryAsync<ReadMovementDto, ReadCategoryDto, ReadMovementDto>(query, (movement, category) =>
            {
                movement.Category = category;
                return movement;
            }, parameters);
        }

        public async Task<IEnumerable<ReadMovementDto>> FindAllByCategoryAsync(int categoryId, int userId)
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
WHERE [m].category_id = @CategoryId
AND [m].user_id = @UserId;";

            return await _conn.QueryAsync<ReadMovementDto, ReadCategoryDto, ReadMovementDto>(query, (movement, category) =>
            {
                movement.Category = category;
                return movement;
            }, new { CategoryId = categoryId, UserId =  userId});
        }

        public async Task<ReadMovementDto?> FindByIdAsync(int id, int userId)
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
WHERE [m].id = @Id
AND [m].user_id = @UserId;";

            return (await _conn.QueryAsync<ReadMovementDto, ReadCategoryDto, ReadMovementDto?>(query, (movement, category) =>
            {
                movement.Category = category;
                return movement;
            }, new { Id = id, UserId = userId })).FirstOrDefault();
        }

        public async Task<ReadMovementDto> InsertAsync(CreateMovementDto movement)
        {
            string insert = @$"
INSERT INTO Movement
(amount, title, date, type, category_id, description, user_id) VALUES
(@Amount, @Title, @Date, @Type, @CategoryId, @Description, @UserId)

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

        public async Task<decimal> SumAllMovementsAmount(int userId)
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
FROM Movement
WHERE user_id = @UserId;
";
            return decimal.Round(await _conn.QueryFirstAsync<decimal>(query, new { UserId = userId }), 2);
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
AND user_id = @UserId
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
AND user_id = @UserId
";

            await _conn.ExecuteAsync(update, movements);
        }

        public async Task ReplaceCategory(int oldCategoryId, int newCategoryId, int userId)
        {
            string update = @"
UPDATE Movement
    SET category_id = @NewCategoryId
WHERE category_id = @OldCategoryId
AND user_id = @UserId;
";

            await _conn.ExecuteAsync(update, new { NewCategoryId = newCategoryId, OldCategoryId = oldCategoryId, UserId = userId });
        }

        public async Task<bool> ExistsByIdAsync(int id, int userId)
        {
            string query = @"
SELECT
    CASE
        WHEN EXISTS(
            SELECT 1 FROM Movement WHERE id = @Id AND user_id = @UserId)
        THEN 1
        ELSE 0
    END;
";

            return Convert.ToBoolean(await _conn.ExecuteScalarAsync<int>(query, new { Id = id, UserId = userId }));
        }
    }
}
