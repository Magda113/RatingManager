using Domain.Models;
using Persistence.Context;
using Dapper;

namespace Persistence.Repository
{
    public class SeriLogRepository : ISerilogRepository, ILogs
    {
        private IDapperContext _context;
        public SeriLogRepository(IDapperContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SeriLog>> GetAllAsync()
        {
            var sql = "SELECT * FROM SeriLogs";
            return await _context.CreateConnection().QueryAsync<SeriLog>(sql);
        }
        public async Task<SeriLog> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM SeriLogs WHERE Id = @Id";
            return (await _context.CreateConnection().QueryAsync<SeriLog>(sql, new { Id = id })).FirstOrDefault();
        }
        public Task<int> AddAsync(SeriLog entity) => throw new System.NotImplementedException();
        public Task<bool> DeleteAsync(int id) => throw new System.NotImplementedException();
        public Task<bool> UpdateAsync(SeriLog entity) => throw new System.NotImplementedException();
        public async Task<IEnumerable<LogCountByDay>> GetLogCountByDay()
        {
            var sql = @"
    SELECT 
        CAST(TimeStamp AS DATE) AS Day, 
        COUNT(*) AS Count
    FROM SeriLogs
    GROUP BY CAST(TimeStamp AS DATE)
    ORDER BY Day";
            var results = await _context.CreateConnection().QueryAsync<LogCountByDay>(sql);
            return results;
        }
    }
}
