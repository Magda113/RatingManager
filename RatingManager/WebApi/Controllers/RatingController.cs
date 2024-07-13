using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Persistence.Repository;
using Application.DTO;
using Application.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly ILogger<RatingController> _logger;
        private readonly IRatingService _ratingService;

        public RatingController(ILogger<RatingController> logger, IRatingService ratingService)
        {
            _logger = logger;
            _ratingService = ratingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ratingsDto = await _ratingService.GetAllAsync();
            _logger.LogInformation("Pobrano listę ocen.");
            return Ok(ratingsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ratingDto = await _ratingService.GetRatingByIdAsync(id);
            if (ratingDto == null)
            {
                _logger.LogInformation($"Nie znaleziono oceny o id {id}");
                return NotFound();
            }
            _logger.LogInformation($"Pobrano ocenę {ratingDto.RatingId}.");
            return Ok(ratingDto);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddRatingDto ratingDto)
        {
            var newRating = await _ratingService.AddRatingAsync(ratingDto);
            _logger.LogInformation($"Ocena {newRating.RatingId} została dodana do bazy danych.");
            return CreatedAtAction(nameof(GetById), new { id = newRating.RatingId }, newRating);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRatingDto updateDto)
        {
            if (id != updateDto.RatingId)
            {
                _logger.LogInformation("Błędne zapytanie.");
                return BadRequest();
            }
            var result = await _ratingService.UpdateRatingAsync(id, updateDto);
            if (!result)
            {
                _logger.LogInformation($"Nie znaleziono oceny o id {id}");
                return NotFound();
            }
            _logger.LogInformation($"Ocena o id {id} została zaktualizowana.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _ratingService.DeleteRatingAsync(id);
            if (!result)
            {
                _logger.LogInformation($"Nie znaleziono oceny o id {id}");
                return NotFound();
            }
            _logger.LogInformation($"Ocena o id {id} została usunięta");
            return NoContent();
        }

        [HttpGet("ByCategoryName/{categoryName}")]
        public async Task<IActionResult> GetRatingsByCategoryName(string categoryName)
        {
            var ratings = await _ratingService.GetRatingsByCategoryNameAsync(categoryName);
            if (ratings == null || !ratings.Any())
            {
                _logger.LogInformation($"Nie ma ocen z kategorią {categoryName}");
                return NotFound();
            }
            _logger.LogInformation($"Otrzymano oceny z kategorią {categoryName}");
            return Ok(ratings);
        }

        [HttpGet("ByUserName/{userName}")]
        public async Task<IActionResult> GetRatingsByUserName(string userName)
        {
            var ratings = await _ratingService.GetRatingsByUserNameAsync(userName);
            if (ratings == null || !ratings.Any())
            {
                _logger.LogInformation($"Nie ma ocen dla {userName}");
                return NotFound();
            }
            _logger.LogInformation($"Otrzymano oceny dla {userName}");
            return Ok(ratings);
        }
    }
}
