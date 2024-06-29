using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;

namespace Persistence.Repository
{
    public interface ILogs
    {
        Task<IEnumerable<LogCountByDayDto>> GetLogCountByDay();
    }
}
