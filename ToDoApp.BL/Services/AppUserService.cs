
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.BL.Interfaces;
using ToDoApp.BL.Models;
using ToDoApp.DAL.Entities;

namespace ToDoApp.BL.Services
{
    public class AppUserService : IAppUserService
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _singInManager;

        public AppUserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _singInManager = signInManager;
        }

        public async Task<IdentityResult> CreateUserAsync(UserSignUpModel model)
        {
            var applicationUser = new AppUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };

            return await _userManager.CreateAsync(applicationUser, model.Password);   
        }

        public async Task<AppUser> LoginAsync(LoginModel model)
        {
            AppUser user = await _userManager.FindByNameAsync(model.UserName);
            
            if(user!=null && await _userManager.CheckPasswordAsync(user, model.Password)){
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserViewModel> GetUserByIdAsync(string userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            UserViewModel result = new UserViewModel
            {
                FullName = user.FullName,
                Id = user.Id
            };
            return result;
        }
    }
}
