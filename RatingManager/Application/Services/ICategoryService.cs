using Application.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICategoryService
    {
        Task<Category> AddCategoryAsync(AddCategoryDto categoryDto);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetCategoryByIdAsync(int categoryId);
        Task<bool> UpdateCategoryAsync(int categoryId, UpdateCategoryDto updateDto);
        Task<bool> DeleteCategoryAsync(int categoryId);
        
    }
}
