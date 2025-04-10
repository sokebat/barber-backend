using BarberApp.Domain;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberApp.Application.Interface
{
   public interface IAuthRepository
    {
        Task<string> Register(Register model);
        //Task<string> Login(Login model);
        Task<(string Token, string FullName)> Login(Login model);

    }
}
