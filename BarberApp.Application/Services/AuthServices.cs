using BarberApp.Application.Interface;
using BarberApp.Domain;
using BarberApp.Domain.Models;
using System.Threading.Tasks;

namespace BarberApp.Application.Services
{
    public class AuthServices : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthServices(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<UserResponse> Register(Register model)
        {
            return await _authRepository.Register(model);
        }

        public async Task<UserResponse> Login(Login model)
        {
            return await _authRepository.Login(model);
        }

        public async Task<UserResponse> GetUserProfile(string userId)
        {
            return await _authRepository.GetUserProfile(userId);
        }
    }
}