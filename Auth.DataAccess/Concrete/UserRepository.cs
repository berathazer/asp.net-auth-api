using Auth.DataAccess.Abstract;
using Auth.Entities;
using Microsoft.EntityFrameworkCore;


namespace Auth.DataAccess.Concrete
{
    public class UserRepository : IUserRepository
    {

        public async Task<List<User>> GetAllUsers()
        {
            using(var AuthDbContext = new AuthDbContext())
            {
                return await AuthDbContext.Users.ToListAsync();
            }
        }

        public async Task<User> GetUserById(Guid id)
        {
            using (var AuthDbContext = new AuthDbContext())
            {
                return await AuthDbContext.Users.FindAsync(id);
            }
        }

        public async Task<User> GetUserByUsername(string username)
        {
            using (var AuthDbContext = new AuthDbContext())
            {
                return await AuthDbContext.Users.FirstOrDefaultAsync(u => u.username == username);

            }
        }

        public  async Task<User> CreateUser(User user)
        {
            using (var AuthDbContext = new AuthDbContext())
            {
                AuthDbContext.Users.Add(user);
                await AuthDbContext.SaveChangesAsync();
                return user;

            }
                
        }

    }
}
