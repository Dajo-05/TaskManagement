using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.DTOs;
using TaskManagement.Entities;
using TaskManagement.Services;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _service;
        private readonly AppDbContext _context;

        public TasksController(TaskService service, AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(TaskDto dto)
        {
            await _service.Create(dto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? status)
        {
            var query = _context.Tasks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(t => t.Status.ToLower() == status.ToLower());
            }

            var result = await query
                .OrderByDescending(t => t.CreatedAt) // 🔥 bonus requerido por la prueba
                .ToListAsync();

            return Ok(result);
        }

       // [HttpPut("{id}/status")]
       // public async Task<IActionResult> ChangeStatus(int id, [FromBody] string status)
        //{
           // await _service.ChangeStatus(id, status);
           // return Ok();
       // }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] string status)
        {
            var result = await _service.ChangeStatus(id, status);

            if (!result.Success)
            {
                return BadRequest(new
                {
                    message = result.Message
                });
            }

            return Ok(new
            {
                message = result.Message
            });
        }
    }
}
