using Dapper;
using Domain.Models;
using Persistence.Context;

namespace Persistence.Repository
{
    public class RatingRepository : IRatingRepository
    {
        private IDapperContext _context;
        public RatingRepository(IDapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Rating>> GetAllAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Rating>("SELECT * FROM Ratings");
            }
        }
        public async Task<int> AddAsync(Rating rating)
        {
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>("INSERT INTO Ratings (Status, CreatedBy, CreatedAt, CallId, UserId, Safety, Knowledge, Communication, Creativity, TechnicalAspects, Result, CategoryId) VALUES (1, @CreatedBy, getdate(), @CallId, @UserId, @Safety, @Knowledge, @Communication, @Creativity, @TechnicalAspects, @Result, @CategoryId); SELECT CAST(SCOPE_IDENTITY() as int)", rating);
                return id;
            }
        }
        public async Task<Rating> GetByIdAsync(int ratingId)
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<Rating>("SELECT * FROM Ratings WHERE RatingId = @RatingId", new { RatingId = ratingId });
            }
        }
        public async Task<bool> UpdateAsync(Rating rating)
        {
            var sql = "UPDATE Ratings SET Status = @Status, ModifiedBy = @ModifiedBy, ModifiedAt = getdate(), CallId = @CallId, UserId = @UserId, Safety = @Safety, Knowledge = @Knowledge, Communication = @Communication, Creativity = @Creativity, TechnicalAspects = @TechnicalAspects,  Result = @Result, CategoryId = @CategoryId  WHERE RatingId = @RatingId";
            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(sql, rating);
                return affectedRows > 0;
            }
        }
        public async Task<bool> DeleteAsync(int ratingId)
        {
            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync("DELETE FROM Ratings WHERE RatingId = @RatingId", new { RatingId = ratingId });
                return affectedRows > 0;
            }
        }
        public async Task<IEnumerable<Rating>> GetRatingsByCategoryNameAsync(string categoryName)
        {
            var sql = @"
            SELECT r.*
            FROM Ratings r
            INNER JOIN Categories c ON r.CategoryId = c.CategoryId
            WHERE c.Name = @CategoryName";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Rating>(sql, new { CategoryName = categoryName });
            }
        }
        public async Task<IEnumerable<Rating>> GetRatingsByUserNameAsync(string userName)
        {
            var sql = @"
            SELECT r.*
            FROM Ratings r
            INNER JOIN Users u ON r.UserId = u.UserId
            WHERE u.UserName = @UserName";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Rating>(sql, new { UserName = userName });
            }
        }
    }
}
