using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Application.DTO;

namespace Application.Services
{
    public interface IRatingService
    {
        Task<Rating>AddRatingAsync(AddRatingDto ratingDto);
        Task<IEnumerable<GetRatingDto>> GetAllAsync();
        Task<GetRatingDto> GetRatingByIdAsync(int ratingId);
        Task<bool> UpdateRatingAsync(int ratingId, UpdateRatingDto updateDto);
        Task<bool> DeleteRatingAsync(int ratingId);
        Task<IEnumerable<GetRatingDto>> GetRatingsByCategoryNameAsync(string categoryName);
        Task<IEnumerable<GetRatingDto>> GetRatingsByUserNameAsync(string userName);
    }
}
