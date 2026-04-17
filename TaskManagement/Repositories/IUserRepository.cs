using TaskManagement.Entities;

namespace TaskManagement.Repositories
{
    public interface IUserRepository
    {
        Task Add(User user);
        Task<List<User>> GetAll();
    }
}
