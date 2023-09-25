using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Identity.Base.Areas.Admin.Models.Dto;
using Identity.Base.Areas.Admin.Models.Dto.Roles;
using Identity.Base.Models.Dto;
using Identity.Base.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity.Base.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public UsersController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }

        public IActionResult Index()
        {
            var users = _userManager.Users
                .Select(p => new UserListDto
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    UserName = p.UserName,
                    PhoneNumber = p.PhoneNumber,
                    EmailConfirmed = p.EmailConfirmed,
                    AccessFailedCount = p.AccessFailedCount
                }).ToList();
            return View(users);
        }


        public IActionResult Create()
        {
      
            return View();
        }

        [HttpPost]
        public IActionResult Create(RegisterDto register)
        {
            if (ModelState.IsValid == false)
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

            var result = _userManager.CreateAsync(newUser, register.Password).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "users", new { area = "admin" });
            }

            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;
            return View(register);
        }


        public IActionResult Edit(string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;

            UserEditDto userEdit = new UserEditDto()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
            };
            return View(userEdit);

        }


        [HttpPost]
        public IActionResult Edit(UserEditDto userEdit)
        {
            var user = _userManager.FindByIdAsync(userEdit.Id).Result;
            user.FirstName = userEdit.FirstName;
            user.LastName = userEdit.LastName;
            user.PhoneNumber = userEdit.PhoneNumber;
            user.Email = userEdit.Email;
            user.UserName = userEdit.UserName;

           var result=  _userManager.UpdateAsync(user).Result;

            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Users", new { area = "Admin" });
            }
            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;
            return View(userEdit);
        }

        public IActionResult Delete(string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;
            UserDeleteDto userDelete = new UserDeleteDto()
            {
                Email = user.Email,
                FullName = $"{user.FirstName}  {user.LastName}",
                Id = user.Id,
                UserName = user.UserName,
            };
            return View(userDelete);
        }

        [HttpPost]
        public IActionResult Delete(UserDeleteDto  userDelete)
        {
            var user = _userManager.FindByIdAsync(userDelete.Id).Result;

           var result=  _userManager.DeleteAsync(user).Result;

            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Users", new { area = "Admin" });

            }

            string message = "";
            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + Environment.NewLine;
            }
            TempData["Message"] = message;
          
            return View(userDelete);
        }

  
        public IActionResult AddUserRole(string Id)
        {

            var user = _userManager.FindByIdAsync(Id).Result;

            var roles = new List<SelectListItem>(
                _roleManager.Roles.Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = p.Name,
                }
                ).ToList());

            return View(new AddUserRoleDto
            {
                 Id=Id,
                 Roles=roles,
                 Email=user.Email,
                 FullName=$"{user.FirstName}  {user.LastName}"
            }); 
        }

        [HttpPost]
        public IActionResult AddUserRole(AddUserRoleDto newRole)
        {
            var user = _userManager.FindByIdAsync(newRole.Id).Result;
            var result = _userManager.AddToRoleAsync(user, newRole.Role).Result;
            return RedirectToAction("UserRoles","Users" , new {Id=user.Id, area="admin"});
        }

        public IActionResult UserRoles(string Id)
        {
            var user = _userManager.FindByIdAsync(Id).Result;
           var roles= _userManager.GetRolesAsync(user).Result;
            ViewBag.UserInfo= $"Name : {user.FirstName } {user.LastName} Email:{user.Email}";
            return View(roles);

           
        }



    }
}
