using Backend.Models.Identity;

namespace Backend.Repositories.Identity
{
    public interface IIdentityRepository
    {
        Task<IEnumerable<User>> GetListAsync();
        Task<User> GetByIdAsync(int id);
        Task SaveUserAsync(User user);

    }
}
