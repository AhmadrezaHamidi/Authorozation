using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Bugeto.Models.Dto;
using Identity.Bugeto.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Bugeto.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterDto register)
        {
            if(ModelState.IsValid ==false)
            {
                return View(register);
            }

            User newUser = new User()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                Email = register.Email,
                UserName = register.Email,
            };

           var result= _userManager.CreateAsync(newUser, register.Password).Result;
            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            string message="";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;
            return View(register);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {

            return View( new LoginDto
            {
                 ReturnUrl=returnUrl,
            });
        }

        [HttpPost]
        public IActionResult Login(LoginDto login)
        {
            if(!ModelState.IsValid)
            {
                return View(login);
            }

            var user = _userManager.FindByNameAsync(login.UserName).Result;

            _signInManager.SignOutAsync();

           var result= _signInManager.PasswordSignInAsync(user, login.Password, login.IsPersistent
                , true).Result;

            if(result.Succeeded == true)
            {
                return Redirect(login.ReturnUrl);
            }
            if(result.RequiresTwoFactor ==true)
            {
                //
            } 
            if(result.IsLockedOut)
            {
                //
            }
           
            ModelState.AddModelError(string.Empty, "Login  Error");
            return View();
        }


        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "home");
        }
    }
}
