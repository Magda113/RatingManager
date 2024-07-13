using Domain.Models;


namespace Persistence.Repository
{
    public interface IRatingRepository
    {
        Task<IEnumerable<Rating>> GetAllAsync();
        Task<Rating> GetByIdAsync(int id);
        Task<int> AddAsync(Rating rating);
        Task<bool> UpdateAsync(Rating rating);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Rating>> GetRatingsByCategoryNameAsync(string categoryName);
        Task<IEnumerable<Rating>> GetRatingsByUserNameAsync(string userName);
    }
}
