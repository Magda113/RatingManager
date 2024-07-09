using Application.Auth;
using Application.DTO;
using Domain.Models;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly Auth.JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(ICategoryRepository repository, JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = repository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<Category> AddCategoryAsync(AddCategoryDto category)
        {
            // Pobranie UserId z JWT
            int userId = _jwtTokenService.GetUserIdFromToken(_httpContextAccessor.HttpContext);

            var newCategory = new Category()
            {
                Name = category.Name,
                CreatedBy = userId,
                Status = CategoryStatus.Active,
                CreatedAt = DateTime.Now
            };

            newCategory.CategoryId = await _repository.AddAsync(newCategory);
            return newCategory;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _repository.GetByIdAsync(categoryId);
        }

        public async Task<bool> UpdateCategoryAsync(int categoryId, UpdateCategoryDto categoryDto)
        {
            int userId = _jwtTokenService.GetUserIdFromToken(_httpContextAccessor.HttpContext);
            var category = await _repository.GetByIdAsync(categoryId);
            if (category == null)
            {
                return false;
            }

            category.Name = categoryDto.Name;
            category.Status = categoryDto.Status;
            category.ModifiedBy = userId;
            category.ModifiedAt = DateTime.Now;

            return await _repository.UpdateAsync(category);
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            return await _repository.DeleteAsync(categoryId);
        }
    }

}
