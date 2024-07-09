using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task<int> AddAsync(Category entity);
        Task<bool> UpdateAsync(Category entity);
        Task<bool> DeleteAsync(int id);
        Task<Category> GetByNameAsync(string categoryName);
    }
}
