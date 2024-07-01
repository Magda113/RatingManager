using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Models;


namespace Persistence.Repository
{
    public interface IRatingRepository
    {
        Task<IEnumerable<RatingDto>> GetRatingsByCategoryNameAsync(string categoryName);
    }
}
