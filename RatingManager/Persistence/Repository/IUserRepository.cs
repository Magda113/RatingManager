using Domain.Models;

//using IUser = Domain.Models.IUser;

namespace Persistence.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<int> AddAsync(User entity);
        Task<bool> UpdateAsync(User entity);
        Task<bool> DeleteAsync(int id);
        Task<User?> Authenticate(string userName, string password);

        Task<User> GetByUserNameAsync(string userName);
    }
}
