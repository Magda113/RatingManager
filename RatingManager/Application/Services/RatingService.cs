using Application.Auth;
using Application.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly Auth.JwtTokenService _jwtTokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RatingService(IRatingRepository ratingRepository, JwtTokenService jwtTokenService, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, ICategoryRepository categoryRepository)
        {
            _ratingRepository = ratingRepository;
            _httpContextAccessor = httpContextAccessor;
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Rating> AddRatingAsync(AddRatingDto ratingDto)
        {
            int createdByUserId = _jwtTokenService.GetUserIdFromToken(_httpContextAccessor.HttpContext);

            var user = await _userRepository.GetByUserNameAsync(ratingDto.UserName);
            if (user == null)
            {
                throw new Exception("Użytkownik nie istnieje");
            }

            var category = await _categoryRepository.GetByNameAsync(ratingDto.CategoryName);
            if (category == null)
            {
                throw new Exception("Kategoria nie istnieje");
            }

            var newRating = new Rating
            {
                Status = RatingStatus.Opublikowana,
                CreatedBy = createdByUserId,
                CreatedAt = DateTime.Now,
                CallId = ratingDto.CallId,
                UserId = user.UserId,
                Safety = ratingDto.Safety,
                Knowledge = ratingDto.Knowledge,
                Communication = ratingDto.Communication,
                Creativity = ratingDto.Creativity,
                TechnicalAspects = ratingDto.TechnicalAspects,
                Result = ratingDto.Result,
                CategoryId = category.CategoryId
            };

            newRating.RatingId = await _ratingRepository.AddAsync(newRating);
            return newRating;
        }

        public async Task<bool> UpdateRatingAsync(int ratingId, UpdateRatingDto updateDto)
        {
            int modifiedByUserId = _jwtTokenService.GetUserIdFromToken(_httpContextAccessor.HttpContext);

            var user = await _userRepository.GetByUserNameAsync(updateDto.UserName);
            if (user == null)
            {
                throw new Exception("Użytkownik nie istnieje");
            }

            var category = await _categoryRepository.GetByNameAsync(updateDto.CategoryName);
            if (category == null)
            {
                throw new Exception("Kategoria nie istnieje");
            }

            var rating = await _ratingRepository.GetByIdAsync(ratingId);
            if (rating == null)
            {
                return false;
            }

            if (!Enum.TryParse<RatingStatus>(updateDto.Status, out var parsedStatus))
            {
                throw new ArgumentException("Invalid status value");
            }

            rating.Status = parsedStatus;
            rating.ModifiedBy = modifiedByUserId;
            rating.ModifiedAt = DateTime.Now;
            rating.CallId = updateDto.CallId;
            rating.UserId = user.UserId;
            rating.Safety = updateDto.Safety;
            rating.Knowledge = updateDto.Knowledge;
            rating.Communication = updateDto.Communication;
            rating.Creativity = updateDto.Creativity;
            rating.TechnicalAspects = updateDto.TechnicalAspects;
            rating.Result = updateDto.Result;
            rating.CategoryId = category.CategoryId;

            return await _ratingRepository.UpdateAsync(rating);
        }

        public async Task<IEnumerable<GetRatingDto>> GetAllAsync()
        {
            var ratings = await _ratingRepository.GetAllAsync();
            var ratingsDto = new List<GetRatingDto>();

            foreach (var rating in ratings)
            {
                var createdByUser = await _userRepository.GetByIdAsync(rating.CreatedBy);
                var modifiedByUser = rating.ModifiedBy.HasValue ? await _userRepository.GetByIdAsync(rating.ModifiedBy.Value) : null;
                var userName = await _userRepository.GetByIdAsync(rating.UserId);
                var categoryName = await _categoryRepository.GetByIdAsync(rating.CategoryId);

                ratingsDto.Add(new GetRatingDto
                {
                    RatingId = rating.RatingId,
                    Status = rating.Status.ToString(),
                    CreatedByUserName = createdByUser?.UserName ?? "Brak",
                    CreatedAt = rating.CreatedAt,
                    ModifiedByUserName = modifiedByUser?.UserName,
                    ModifiedAt = rating.ModifiedAt,
                    CallId = rating.CallId,
                    UserName = userName?.UserName ?? "Brak",
                    Safety = rating.Safety,
                    Knowledge = rating.Knowledge,
                    Communication = rating.Communication,
                    Creativity = rating.Creativity,
                    TechnicalAspects = rating.TechnicalAspects,
                    Result = rating.Result,
                    CategoryName = categoryName?.Name ?? "Brak"
                });
            }

            return ratingsDto;
        }

        public async Task<GetRatingDto> GetRatingByIdAsync(int ratingId)
        {
            var rating = await _ratingRepository.GetByIdAsync(ratingId);
            
            var createdByUser = await _userRepository.GetByIdAsync(rating.CreatedBy);
            var modifiedByUser = rating.ModifiedBy.HasValue ? await _userRepository.GetByIdAsync(rating.ModifiedBy.Value) : null;
            var userName = await _userRepository.GetByIdAsync(rating.UserId);
            var categoryName = await _categoryRepository.GetByIdAsync(rating.CategoryId);

            var ratingDto = new GetRatingDto
            {
                RatingId = rating.RatingId,
                Status = rating.Status.ToString(),
                CreatedByUserName = createdByUser?.UserName?? "Brak",
                CreatedAt = rating.CreatedAt,
                ModifiedByUserName = modifiedByUser?.UserName,
                ModifiedAt = rating.ModifiedAt,
                CallId = rating.CallId,
                UserName = userName?.UserName ?? "Brak",
                Safety = rating.Safety,
                Knowledge = rating.Knowledge,
                Communication = rating.Communication,
                Creativity = rating.Creativity,
                TechnicalAspects = rating.TechnicalAspects,
                Result = rating.Result,
                CategoryName = categoryName?.Name ?? "Brak"
            };

            return ratingDto;
        }
        
        public async Task<bool> DeleteRatingAsync(int ratingId)
        {
            return await _ratingRepository.DeleteAsync(ratingId);
        }

        public async Task<IEnumerable<GetRatingDto>> GetRatingsByCategoryNameAsync(string category)
        {
            var ratings = await _ratingRepository.GetRatingsByCategoryNameAsync(category);
            var ratingsDto = new List<GetRatingDto>();


            foreach (var rating in ratings)
            {
                var createdByUser = await _userRepository.GetByIdAsync(rating.CreatedBy);
                var modifiedByUser = rating.ModifiedBy.HasValue ? await _userRepository.GetByIdAsync(rating.ModifiedBy.Value) : null;
                var userName = await _userRepository.GetByIdAsync(rating.UserId);
                var categoryName = await _categoryRepository.GetByIdAsync(rating.CategoryId);

                ratingsDto.Add(new GetRatingDto
                {
                    RatingId = rating.RatingId,
                    Status = rating.Status.ToString(),
                    CreatedByUserName = createdByUser?.UserName ?? "Brak",
                    CreatedAt = rating.CreatedAt,
                    ModifiedByUserName = modifiedByUser?.UserName,
                    ModifiedAt = rating.ModifiedAt,
                    CallId = rating.CallId,
                    UserName = userName?.UserName ?? "Brak",
                    Safety = rating.Safety,
                    Knowledge = rating.Knowledge,
                    Communication = rating.Communication,
                    Creativity = rating.Creativity,
                    TechnicalAspects = rating.TechnicalAspects,
                    Result = rating.Result,
                    CategoryName = categoryName?.Name ?? "Brak"
                });
            }

            return ratingsDto;
        }

        public async Task<IEnumerable<GetRatingDto>> GetRatingsByUserNameAsync(string user)
        {
            var ratings = await _ratingRepository.GetRatingsByUserNameAsync(user);
            var ratingsDto = new List<GetRatingDto>();

            foreach (var rating in ratings)
            {
                var createdByUser = await _userRepository.GetByIdAsync(rating.CreatedBy);
                var modifiedByUser = rating.ModifiedBy.HasValue ? await _userRepository.GetByIdAsync(rating.ModifiedBy.Value) : null;
                var userName = await _userRepository.GetByIdAsync(rating.UserId);
                var categoryName = await _categoryRepository.GetByIdAsync(rating.CategoryId);

                ratingsDto.Add(new GetRatingDto
                {
                    RatingId = rating.RatingId,
                    Status = rating.Status.ToString(),
                    CreatedByUserName = createdByUser?.UserName ?? "Brak",
                    CreatedAt = rating.CreatedAt,
                    ModifiedByUserName = modifiedByUser?.UserName,
                    ModifiedAt = rating.ModifiedAt,
                    CallId = rating.CallId,
                    UserName = userName?.UserName ?? "Brak",
                    Safety = rating.Safety,
                    Knowledge = rating.Knowledge,
                    Communication = rating.Communication,
                    Creativity = rating.Creativity,
                    TechnicalAspects = rating.TechnicalAspects,
                    Result = rating.Result,
                    CategoryName = categoryName?.Name ?? "Brak"
                });
            }

            return ratingsDto;
        }
    }

}
