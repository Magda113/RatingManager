using Persistence.Context;
using Domain.Models;
using Dapper;

namespace Persistence.Repository
{
    public class UserRepository : IUserRepository
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
                var users = await connection.QueryAsync<User>("SELECT UserId, UserName, Email, Role, Department, CreatedBy, CreatedAt, ModifiedBy, ModifiedAt FROM Users");
                return users;
            }
        }

        public async Task<int> AddAsync(User entity)
        {
            var sql = "";
            if (_context is DapperContext)
            {
                sql = "INSERT INTO Users (UserName, Email, Role, Department, CreatedBy, CreatedAt, PasswordHash) VALUES (@UserName, @Email, @Role, @Department, @CreatedBy, getdate(), @PasswordHash); SELECT CAST(SCOPE_IDENTITY() as int)";
            }
            else
            {
                sql = "INSERT INTO Users (UserName, Email, Role, Department, CreatedBy, CreatedAt, PasswordHash) VALUES (@UserName, @Email, @Role, @Department, @CreatedBy, getdate(), @PasswordHash); SELECT CAST(last_insert_rowid() as int)";
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
                var user = await connection.QuerySingleOrDefaultAsync<User>("SELECT UserId, UserName, Email, Role, Department, CreatedBy, CreatedAt, ModifiedBy, ModifiedAt FROM Users WHERE UserId = @UserId", new { UserId = userId });
                return user;
            }
        }

        public async Task<bool> UpdateAsync(User entity)
        {
            var sql = "UPDATE Users SET UserName = @UserName, Email = @Email, Role = @Role, Department = @Department, ModifiedBy = @ModifiedBy, ModifiedAt = getdate() WHERE UserId = @UserId";
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

        public async Task<User> GetByUserNameAsync(string userName)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<User>(
                    "SELECT * FROM Users WHERE UserName = @UserName",
                    new { UserName = userName.ToLower() });
            }
        }

    }

}
