using Persistence.Context;
using Domain.Models;
using Domain.DTO;
using Dapper;

namespace Persistence.Repository
{
    public class UserRepository : IRepository<User>, IUserRepository
    {
        private IDapperContext _context;
        public UserRepository(IDapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<User>("SELECT * FROM Users");
            }
        }
        public async Task<int> AddAsync(User entity)
        {
            var sql = "";
            if (_context is DapperContext)
            {
                sql = "INSERT INTO Users (UserName, Email, Role, Department, CreatedAt, PasswordHash) VALUES (@UserName, @Email, @Role, @Department, getdate(), @PasswordHash); SELECT CAST(SCOPE_IDENTITY() as int)";
            }
            else
            {
                sql = "INSERT INTO Users (UserName, Email, Role, Department, CreatedAt, PasswordHash) VALUES (@UserName, @Email, @Role, @Department, getdate(), @PasswordHash); SELECT CAST(last_insert_rowid() as int)";
            }
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(sql, entity);
                return id;
            }
        }
        public async Task<User> GetByIdAsync(int userId)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<User>("SELECT * FROM Users WHERE UserId = @UserId", new { UserId = userId });
            }
        }
        public async Task<bool> UpdateAsync(User entity)
        {
            var sql = "UPDATE Users SET UserName = @UserName, Email = @Email, Role = @Role, Department = @Department, PasswordHash = @PasswordHash, ModifiedBy = @ModifiedBy, ModifiedAt = getdate() WHERE UserId = @UserId";
            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows > 0;
            }
        }
        public async Task<bool> DeleteAsync(int userId)
        {
            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync("DELETE FROM Users WHERE UserId = @UserId", new { UserId = userId });
                return affectedRows > 0;
            }
        }

        public async Task<User?> Authenticate(string userName, string password)
        {
            using (var connection = _context.CreateConnection())
            {
                var sql = "SELECT UserId, UserName, Email, Role FROM Users WHERE UserName = @UserName AND PasswordHash = @Password";
                var user = await connection.QuerySingleOrDefaultAsync<User>(sql, new { userName, password });
                return user;
            }
        }
    }
}
