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
        Task<GetCategoryDto> AddCategoryAsync(AddCategoryDto categoryDto);
        Task<IEnumerable<GetCategoryDto>> GetAllAsync();
        Task<GetCategoryDto> GetCategoryByIdAsync(int categoryId);
        Task<bool> UpdateCategoryAsync(int categoryId, UpdateCategoryDto updateDto);
        Task<bool> DeleteCategoryAsync(int categoryId);
    }
}
