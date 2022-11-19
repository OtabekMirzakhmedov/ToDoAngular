using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.BL.Models;
using ToDoApp.DAL.Entities;

namespace ToDoApp.BL.Interfaces
{
    public interface IAppUserService
    {
        Task<IdentityResult> CreateUserAsync(UserSignUpModel model);
        Task<AppUser> LoginAsync(LoginModel model);

        Task<UserViewModel> GetUserByIdAsync(string userId);
    }
}
