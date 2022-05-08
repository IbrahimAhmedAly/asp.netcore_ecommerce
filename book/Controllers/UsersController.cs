using book.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book.Controllers
{
    public class UsersController : Controller
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signinManager;
        public UsersController(UserManager<IdentityUser> manager , SignInManager<IdentityUser> signManager)
        {
            userManager = manager;
            signinManager = signManager;
        }
        public IActionResult Login()
        {      
            return View(new UserModel());
        }
        public async Task<IActionResult> LogOut()
        {
            await signinManager.SignOutAsync();
            return Redirect("~/");
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            var user = new IdentityUser
            {
                Email = userModel.Email,
                UserName = userModel.Email
            };
            var result = await userManager.CreateAsync(user, userModel.Password);
            if (result.Succeeded)
            {
                return Redirect("~/");
            }
            else
                return View("Login",userModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserModel userModel)
        {

            var result = await signinManager.PasswordSignInAsync(userModel.Email, userModel.Email, true, true); 
            if (result.Succeeded)
            {
                return Redirect("~/");
            }
            else
                return View("Login", userModel);
        }
    }
}
