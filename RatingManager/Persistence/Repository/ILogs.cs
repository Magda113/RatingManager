using Domain.Models;


namespace Persistence.Repository
{
    public interface ILogs
    {
        Task<IEnumerable<LogCountByDay>> GetLogCountByDay();
    }
}
