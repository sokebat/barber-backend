using BarberApp.Domain;


namespace BarberApp.Application.Interface
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task AddUser(User user);
    }
}
