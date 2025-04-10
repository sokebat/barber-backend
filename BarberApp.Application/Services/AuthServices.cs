using BarberApp.Application.Interface;
using BarberApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberApp.Application.Services
{
    //public class AuthServices : IAuthService
    //{
    //    private readonly IAuthRepository _authRepository;
    //    public AuthServices(IAuthRepository authRepository)
    //    {
    //        _authRepository = authRepository;
    //    }
    //    public async Task<string> Login(Login model)
    //    {
    //        return await _authRepository.Login(model);
    //    }

    //    public async Task<string> Register(Register model)
    //    {
    //        return await _authRepository.Register(model);
    //    }
    //}
    public class AuthServices : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthServices(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<(string Token, string FullName)> Login(Login model)
        {
            return await _authRepository.Login(model); // Return tuple
        }

        public async Task<string> Register(Register model)
        {
            return await _authRepository.Register(model);
        }
    }
}
