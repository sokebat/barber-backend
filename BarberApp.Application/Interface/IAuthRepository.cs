 
using BarberApp.Domain.Models;
 

namespace BarberApp.Application.Interface
{
    public interface IAuthRepository
    {
        Task<UserResponse> Register(Register model);
        Task<UserResponse> Login(Login model);
        Task<UserResponse> GetUserProfile(string userId);
    }
}
