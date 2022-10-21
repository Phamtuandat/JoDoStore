using Backend.Data;
using Backend.Models.Identity;
using Microsoft.EntityFrameworkCore;


namespace Backend.Repositories.Identity
{
    public class IdentityRepository : BaseRepository, IIdentityRepository
    {
        public IdentityRepository(DataContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetListAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task SaveUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

    }
}
