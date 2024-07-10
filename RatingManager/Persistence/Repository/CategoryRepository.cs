using Dapper;
using Domain.Models;
using Persistence.Context;

namespace Persistence.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private IDapperContext _context;
        public CategoryRepository(IDapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Category>("SELECT * FROM Categories");
            }
        }
        public async Task<int> AddAsync(Category entity)
        {
            var sql = "";
            if (_context is DapperContext)
            {
                sql =
                    "INSERT INTO Categories (Name, Status, CreatedBy, CreatedAt) VALUES (@Name, 1, @CreatedBy, getdate()); SELECT CAST(SCOPE_IDENTITY() as int)";
            }
            else
            {
                sql = "INSERT INTO Categories (Name, Status, CreatedBy, CreatedAt, ModifiedBy, ModifiedAt) VALUES (@Name, 1, @CreatedBy, getdate()); SELECT CAST(last_insert_rowid() as int)";
            }
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(sql, entity);
                return id;
            }
        }
        public async Task<Category> GetByIdAsync(int categoryId)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Category>("SELECT * FROM Categories WHERE CategoryId = @CategoryId", new { CategoryId = categoryId });
            }
        }
        public async Task<bool> UpdateAsync(Category entity)
        {
            var sql = "UPDATE Categories SET Name = @Name, Status = @Status, ModifiedBy = @ModifiedBy, ModifiedAt = getdate() WHERE CategoryId = @CategoryId";
            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows > 0;
            }
        }
        public async Task<bool> DeleteAsync(int categoryId)
        {
            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync("DELETE FROM Categories WHERE CategoryId = @CategoryId", new { CategoryId = categoryId });
                return affectedRows > 0;
            }
        }

        public async Task<Category> GetByNameAsync(string categoryName)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Category>(
                    "SELECT * FROM Categories WHERE Name = @Name",
                    new { Name = categoryName.ToLower() });
            }
        }
    }
}
