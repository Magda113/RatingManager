using Dapper;
using Domain.Models;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class RatingRepository : IRepository<Rating>
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
        public async Task<int> AddAsync(Rating entity)
        {
            var sql = "";
            if (_context is DapperContext)
            {
                sql = "INSERT INTO Ratings (Status, CreatedBy, CreatedAt, CallId, UserId, Safety, Knowledge, Communication, Creativity, TechnicalAspects, Result, CategoryId) VALUES (2, @CreatedBy, getdate(), @CallId, @UserId, @Safety, @Knowledge, @Communication, @Creativity, @TechnicalAspects, @Result, @CategoryId); SELECT CAST(SCOPE_IDENTITY() as int)";
            }
            else
            {
                sql = "INSERT INTO Ratings (Status, CreatedBy, CreatedAt, CallId, UserId, Safety, Knowledge, Communication, Creativity, TechnicalAspects, Result, CategoryId) VALUES (2, @CreatedBy, getdate(), @CallId, @UserId, @Safety, @Knowledge, @Communication, @Creativity, @TechnicalAspects, @Result, @CategoryId); SELECT CAST(last_insert_rowid() as int)";
            }
            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(sql, entity);
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
        public async Task<bool> UpdateAsync(Rating entity)
        {
            var sql = "UPDATE Ratings SET Status = @Status, ModifiedBy = @ModifiedBy, ModifiedAt = getdate(), CallId = @CallId, UserId = @UserId, Safety = @Safety, Knowledge = @Knowledge, Communication = @Communication, Creativity = @Creativity, TechnicalAspects = @TechnicalAspects,  Result = @Result, CategoryId = @CategoryId  WHERE RatingId = @RatingId";
            using (var connection = _context.CreateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(sql, entity);
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
    }
}
