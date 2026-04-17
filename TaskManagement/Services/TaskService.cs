using Microsoft.EntityFrameworkCore;
using TaskManagement.DTOs;
using TaskManagement.Entities;

namespace TaskManagement.Services
{
    public class TaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(TaskDto dto)
        {
            if (string.IsNullOrEmpty(dto.Title))
                throw new Exception("El título es obligatorio");

            var userExists = await _context.User.AnyAsync(u => u.Id == dto.UserId);
            if (!userExists)
                throw new Exception("Usuario no existe");

            var task = new TaskItem
            {
                Title = dto.Title,
                UserId = dto.UserId,
                ExtraData = dto.ExtraData,
                Status = "Pending"
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task<(bool Success, string Message)> ChangeStatus(int id, string status)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
                return (false, "La tarea no existe");

            if (task.Status == "Pending" && status == "Done")
                return (false, "No puedes pasar directamente de Pendiente a Completada");

            task.Status = status;
            await _context.SaveChangesAsync();

            return (true, "Estado actualizado correctamente");
        }
    }
}
