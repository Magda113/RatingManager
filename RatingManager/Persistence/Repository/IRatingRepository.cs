using Domain.Models;


namespace Persistence.Repository
{
    public interface IRatingRepository
    {
        Task<IEnumerable<Rating>> GetAllAsync();
        Task<Rating> GetByIdAsync(int id);
        Task<int> AddAsync(Rating entity);
        Task<bool> UpdateAsync(Rating entity);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Rating>> GetRatingsByCategoryNameAsync(string categoryName);

        Task<IEnumerable<Rating>> GetRatingsByUserNameAsync(string categoryName);
    }
}
