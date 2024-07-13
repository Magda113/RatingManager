using Application.DTO;
using Application.Services;
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
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            _logger.LogInformation("Pobrano listę kategorii.");
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
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
        public async Task<IActionResult> Add([FromBody] AddCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newCategory = await _categoryService.AddCategoryAsync(categoryDto);
            _logger.LogInformation($"Kategoria {newCategory.Name} została dodana do bazy danych.");
            return CreatedAtAction(nameof(GetById), new { id = newCategory.CategoryId }, newCategory);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto categoryDto)
        {
            if (id != categoryDto.CategoryId)
            {
                _logger.LogInformation($"Błędne zapytanie.");
                return BadRequest();
            }

            var result = await _categoryService.UpdateCategoryAsync(id, categoryDto);
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
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result)
            {
                _logger.LogInformation($"Nie znaleziono kategorii o id {id}");
                return NotFound();
            }
            _logger.LogInformation($"Kategoria o id {id} została usunięta.");
            return NoContent();
        }
    }
}
