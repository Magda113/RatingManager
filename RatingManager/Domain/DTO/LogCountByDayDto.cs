using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class LogCountByDayDto
    {
        public DateTime Day { get; set; }
        public int Count { get; set; }
    }
}
