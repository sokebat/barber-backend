using BarberApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberApp.Application.Interface
{
   public interface IAuthService
    {
        Task<string> Register(Register model);
        //Task<string> Login(Login model);
        Task<(string Token, string FullName)> Login(Login model);
    }
}
