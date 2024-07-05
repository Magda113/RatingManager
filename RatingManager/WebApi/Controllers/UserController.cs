using Domain.Models;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repository;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _repository;
        private readonly ILogger<UserController> _logger;
        public UserController(IRepository<User> repository,ILogger<UserController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repository.GetAllAsync();
            _logger.LogInformation($"Pobrano listę użytkoników.");
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"Nie znaleziono użytkownika o id {id}");
                return NotFound();
            }
           _logger.LogInformation($"Pobrano użytkownika {user.UserName}.");
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserDto user)
        {
            var newUser = new User()
            {
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                Department = user.Department,
                PasswordHash = user.PasswordHash
            };
            var createdUserId = await _repository.AddAsync(newUser);
            _logger.LogInformation($"Użytkownik {newUser.UserName} został dodany do bazy danych.");
            return CreatedAtAction(nameof(GetById), new { id = createdUserId }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto user)
        {
            if (id != user.UserId)
            {
                _logger.LogInformation($"Błędne zapytanie.");
                return BadRequest();
            }
            var newUser = new User()
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email,
                Department = user.Department,
                Role = user.Role,
                PasswordHash = user.PasswordHash,
                ModifiedBy = user.ModifiedBy
            };
            var result = await _repository.UpdateAsync(newUser);
            if (!result)
            {
                _logger.LogInformation($"Nie znaleziono użytkownika o id {id}");
                return NotFound();
            }
            _logger.LogInformation($"Użytkownik o id {id} został zaaktualizowany.");
            return NoContent(); // Można również zwrócić Ok(user) lub inną odpowiedź w zależności od konwencji
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);
            if (!result)
            {
               _logger.LogInformation($"Nie znaleziono użytkownika o id {id}");
                return NotFound();
            }
           _logger.LogInformation($"Użytkownik o id {id} został usunięty");
            return NoContent();
        }
    }
}