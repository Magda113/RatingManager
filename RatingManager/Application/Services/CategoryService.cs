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
        private readonly IUserRepository _userRepository;

        public CategoryService(ICategoryRepository repository, JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _repository = repository;
            _jwtTokenService = jwtTokenService;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<GetCategoryDto> AddCategoryAsync(AddCategoryDto categoryDto)
        {
            int userId = _jwtTokenService.GetUserIdFromToken(_httpContextAccessor.HttpContext);
            var user = await _userRepository.GetByIdAsync(userId);

            var newCategory = new Category()
            {
                Name = categoryDto.Name,
                CreatedBy = userId,
                Status = CategoryStatus.Aktywna,
                CreatedAt = DateTime.Now
            };

            newCategory.CategoryId = await _repository.AddAsync(newCategory);

            return new GetCategoryDto
            {
                CategoryId = newCategory.CategoryId,
                Name = newCategory.Name,
                Status = newCategory.Status.ToString(),
                CreatedByUserName = user?.UserName ?? "Brak",
                CreatedAt = newCategory.CreatedAt
            };
        }


        public async Task<IEnumerable<GetCategoryDto>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            var categoriesDto = new List<GetCategoryDto>();

            foreach (var category in categories)
            {
                var createdByUser = await _userRepository.GetByIdAsync(category.CreatedBy);
                var modifiedByUser = category.ModifiedBy.HasValue ? await _userRepository.GetByIdAsync(category.ModifiedBy.Value) : null;

                categoriesDto.Add(new GetCategoryDto
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    Status = category.Status.ToString(),
                    CreatedByUserName = createdByUser?.UserName ?? "Brak",
                    CreatedAt = category.CreatedAt,
                    ModifiedByUserName = modifiedByUser?.UserName,
                    ModifiedAt = category.ModifiedAt
                });
            }

            return categoriesDto;
        }

        public async Task<GetCategoryDto> GetCategoryByIdAsync(int categoryId)
        {
            var category = await _repository.GetByIdAsync(categoryId);
            if (category == null) return null;

            var createdByUser = await _userRepository.GetByIdAsync(category.CreatedBy);
            var modifiedByUser = category.ModifiedBy.HasValue ? await _userRepository.GetByIdAsync(category.ModifiedBy.Value) : null;

            var categoryDto = new GetCategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Status = category.Status.ToString(),
                CreatedByUserName = createdByUser?.UserName ?? "Brak",
                CreatedAt = category.CreatedAt,
                ModifiedByUserName = modifiedByUser?.UserName,
                ModifiedAt = category.ModifiedAt
            };

            return categoryDto;
        }

        public async Task<bool> UpdateCategoryAsync(int categoryId, UpdateCategoryDto categoryDto)
        {
            int userId = _jwtTokenService.GetUserIdFromToken(_httpContextAccessor.HttpContext);
            var user = await _userRepository.GetByIdAsync(userId);
            var category = await _repository.GetByIdAsync(categoryId);
            if (category == null)
            {
                throw new Exception("Kategoria nie istnieje");
            }

            if (!Enum.TryParse<CategoryStatus>(categoryDto.Status, true, out var parsedStatus))
            {
                throw new ArgumentException("Invalid status value");
            }

            category.Name = categoryDto.Name;
            category.Status = parsedStatus;
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
