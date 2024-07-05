using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IRepository<Category> _repository;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(IRepository<Category> repository, ILogger<CategoryController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _repository.GetAllAsync();
            _logger.LogInformation($"Pobrano listę kategorii.");
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null)
            {
                _logger.LogInformation($"Nie znaleziono kategorii o id {id}");
                return NotFound();
            }
            _logger.LogInformation($"Pobrano kategorię {category.Name}.");
            return Ok(category);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddCategoryDto category)
        {
            var newCategory = new Category()
            {
                Name = category.Name,
                CreatedBy = category.CreatedBy,
                Status = CategoryStatus.Active
            };
            var createdCategoryId = await _repository.AddAsync(newCategory);
            _logger.LogInformation($"Kategoria {newCategory.Name} została dodana do bazy danych.");
            return CreatedAtAction(nameof(GetById), new { id = createdCategoryId }, category);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto category)
        {
            if (id != category.CategoryId)
            {
                _logger.LogInformation($"Błędne zapytanie.");
                return BadRequest();
            }
            var newCategory = new Category()
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                ModifiedBy = category.ModifiedBy,
                Status = category.Status
            };
            var result = await _repository.UpdateAsync(newCategory);
            if (!result)
            {
                _logger.LogInformation($"Nie znaleziono kategorii o id {id}");
                return NotFound();
            }
            _logger.LogInformation($"Kategoria o id {id} została zaaktualizowana.");
            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (!result)
            {
               _logger.LogInformation($"Nie znaleziono kategorii o id {id}");
                return NotFound();
            }
            _logger.LogInformation($"Kategoria o id {id} została usunięta");
            return NoContent();
        }
    }
}
