using BarberApp.Application.Interface;
using BarberApp.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BarberApp.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BarberDbContext _context;

        public UserRepository(BarberDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            try
            {
                return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch(Exception ex)
            {
                throw new Exception("Email is not registered");

            }
        }

        public async Task AddUser(User user)
        {
            await _context.User.AddAsync(user);
        }
    }
}
