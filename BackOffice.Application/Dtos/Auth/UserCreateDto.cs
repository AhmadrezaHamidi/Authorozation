using BackOffice.Application.Profiles;
using BackOffice.Domain.Entities.Users;
using Duende.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Dtos.Auth
{
    public class UserCreateDto : ICreateMapper<User>
    {
        /// <example>Amirfoad</example>
        [Required(ErrorMessage = "وارد کردن نام کاربری  الزامی است")]
        public string UserName { get; set; }
        /// <example>AmirfoadAhmadi@gmail.com</example>
        [Required(ErrorMessage = "وارد کردن ایمیل  الزامی است")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نیست")]
        public string Email { get; set; }
        /// <example>حمید</example>
        [Required(ErrorMessage = "وارد کردن نام  الزامی است")]
        public string FirstName { get; set; }
        /// <example>احمدی</example>
        [Required(ErrorMessage = "وارد کردن نام خوانوادگی الزامی است")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "وارد کردن رمز عبور  الزامی است")]
        public string Password { get; set; }
        [Required(ErrorMessage = "وارد کردن تأیید رمز عبور  الزامی است")]
        [Compare("Password", ErrorMessage = "رمز عبور و  تأیید رمز عبور باید مطابقت داشته باشند")]
        public string RePassword { get; set; }

    }
}
