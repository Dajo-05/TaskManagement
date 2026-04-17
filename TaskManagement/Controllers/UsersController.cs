using Microsoft.AspNetCore.Mvc;
using TaskManagement.DTOs;
using TaskManagement.Entities;
using TaskManagement.Repositories;

namespace TaskManagement.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserDto dto)
        {
            var user = new User { Name = dto.Name, Email = dto.Email };
            await _repo.Add(user);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _repo.GetAll();
            return Ok(users);
        }
    }
}
