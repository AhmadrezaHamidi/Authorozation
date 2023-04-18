using BackOffice.Application.Profiles;
using BackOffice.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace BackOffice.Application.Dtos.Auth
{
    public class UserUpdateDto : ICreateMapper<User>
    {
        public int Id { get; set; }


        [EmailAddress(ErrorMessage = "ایمیل وارد شده صحیح نیست")]
        public string Email { get; set; }

        [Required(ErrorMessage = "وارد کردن نام  الزامی است")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "وارد کردن نام خوانوادگی الزامی است")]
        public string LastName { get; set; }

        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "رمز عبور و تایید رمز عبور مطابقت ندارند")]
        public string Respassword { get; set; }



    }
}
