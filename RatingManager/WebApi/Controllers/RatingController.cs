using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using Domain.DTO;
using Persistence.Repository;
using System.ComponentModel.DataAnnotations;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRepository<Rating> _repository;
        private readonly ILogger<RatingController> _logger;
        private readonly IRatingRepository _ratingRepository;

        public RatingController(IRepository<Rating> repository, ILogger<RatingController> logger, IRatingRepository ratingRepository)
        {
            _repository = repository;
            _logger = logger;
            _ratingRepository = ratingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ratings = await _repository.GetAllAsync();
           _logger.LogInformation($"Pobrano listę ocen.");
            return Ok(ratings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var rating = await _repository.GetByIdAsync(id);
            if (rating == null)
            {
                _logger.LogInformation($"Nie znaleziono oceny o id {id}");
                return NotFound();
            }
            _logger.LogInformation($"Pobrano ocenę {rating.RatingId}.");
            return Ok(rating);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RatingAddDto rating)
        {
            var newRating = new Rating()
            {
                CallId = rating.CallId,
                UserId = rating.UserId,
                Safety = rating.Safety,
                Knowledge = rating.Knowledge,
                Communication = rating.Communication,
                Creativity = rating.Creativity,
                TechnicalAspects = rating.TechnicalAspects,
                Result = rating.Result,
                CategoryId = rating.CategoryId,
                CreatedBy = rating.CreatedBy
            };
        
        var createdRatingId = await _repository.AddAsync(newRating);
            _logger.LogInformation($"Ocena {newRating.RatingId} została dodana do bazy danych.");
            return CreatedAtAction(nameof(GetById), new { id = createdRatingId }, rating);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RatingUpdateDto rating)
        {
            if (id != rating.RatingId)
            {
               _logger.LogInformation($"Błędne zapytanie.");
                return BadRequest();
            }
            var newRating = new Rating()
            {
                RatingId = rating.RatingId,
               ModifiedBy = rating.ModifiedBy,
               CallId = rating.CallId,
               UserId = rating.UserId,
               Safety = rating.Safety,
               Knowledge = rating.Knowledge,
               Communication = rating.Communication,
               Creativity = rating.Creativity,
               TechnicalAspects = rating.TechnicalAspects,
               Result = rating.Result,
               CategoryId = rating.CategoryId,
               Status = rating.Status
            };
        var result = await _repository.UpdateAsync(newRating);
            if (!result)
            {
                _logger.LogInformation($"Nie znaleziono oceny o id {id}");
                return NotFound();
            }
            _logger.LogInformation($"Ocena o id {id} została zaaktualizowana.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
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
            var ratingIds = await _ratingRepository.GetRatingIdsByCategoryNameAsync(categoryName);
            if (ratingIds == null || !ratingIds.Any())
            {
                _logger.LogInformation($"Nie ma ocen z kategorią {categoryName}");
                return NotFound();
            }

            _logger.LogInformation($"Otrzymano oceny z kategorią {categoryName}");
            return Ok(ratingIds);
        }
    }
}
