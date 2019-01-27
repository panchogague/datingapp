using DateApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DateApp.API.Data
{
    public interface IAuthRepository
    {
        Task<UserModel> Register(UserModel user,string password);
        Task<UserModel> Login(string userName, string password);
        Task<bool> ExistUser(string userName);
    }
}
