using Domain.Models;
using Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repository;
using Application.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            _logger.LogInformation($"Pobrano listę użytkowników.");
            return Ok(users);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                _logger.LogWarning($"Nie znaleziono użytkownika o id {id}");
                return NotFound();
            }
            _logger.LogInformation($"Pobrano użytkownika {user.UserName}.");
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = await _userService.AddUserAsync(userDto);
            _logger.LogInformation($"Użytkownik {newUser.UserName} został dodany do bazy danych.");
            return CreatedAtAction(nameof(GetById), new { id = newUser.UserId }, newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto userDto)
        {
            if (id != userDto.UserId)
            {
                _logger.LogInformation($"Błędne zapytanie.");
                return BadRequest();
            }

            var result = await _userService.UpdateUserAsync(id, userDto);
            if (!result)
            {
                _logger.LogInformation($"Nie znaleziono użytkownika o id {id}");
                return NotFound();
            }

            _logger.LogInformation($"Użytkownik o id {id} został zaaktualizowany.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
            {
                _logger.LogInformation($"Nie znaleziono użytkownika o id {id}");
                return NotFound();
            }

            _logger.LogInformation($"Użytkownik o id {id} został usunięty.");
            return NoContent();
        }

    }

}