using Microsoft.AspNetCore.Mvc;
using Persistence.Repository;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class SeriLogController : ControllerBase
    {
        private SeriLogRepository _repository;
        public SeriLogController(SeriLogRepository logRepository)
        {
            _repository = logRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var logs = await _repository.GetAllAsync();
            return Ok(logs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var log = await _repository.GetByIdAsync(id);
            if (log == null)
            {
                return NotFound();
            }
            return Ok(log);
        }

        [HttpGet("count-by-day")]
        public async Task<IActionResult> GetLogCountByDay()
        {
            var logs = await _repository.GetLogCountByDay();
            return Ok(logs);
        }
    }
}
