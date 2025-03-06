 
using BarberApp.Domain;

namespace BarberApp.Application.Interface
{
    public interface IAuthService
    {
        Task<string> Login(string email, string password);
        Task<User> Register(User user, string password);
    }
}
