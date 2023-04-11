using BackOffice.Application.Contracts;
using BackOffice.Application.Dtos.Auth;
using BackOffice.Persistance.Context;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Persistance.Repositories
{
    public class UserServiceRepository : IUserServiceRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IdentityDbContext _db;

        public UserServiceRepository(UserManager<IdentityUser> userManager, IdentityDbContext db)
        {
            this._userManager = userManager;
            this._db = db;
        }

        public UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user)
        {
            _db.UserRefreshToken.Add(user);
            return user;
        }

        public void DeleteUserRefreshTokens(string username, string refreshToken)
        {
            var item = _db.UserRefreshToken.FirstOrDefault(x => x.UserName == username && x.RefreshToken == refreshToken);
            if (item != null)
            {
                _db.UserRefreshToken.Remove(item);
            }
        }
        public UserRefreshTokens GetSavedRefreshTokens(string username, string refreshToken)
        {
            return _db.UserRefreshToken.FirstOrDefault(x => x.UserName == username && x.RefreshToken == refreshToken && x.IsActive == true);
        }

        public int SaveCommit()
        {
            return _db.SaveChanges();
        }
        public async Task<bool> IsValidUserAsyncForLogin(Users users)
        {

            var u = _userManager.Users.FirstOrDefault(o => o.UserName == users.Name);
            var result = await _userManager.CheckPasswordAsync(u, users.Password);
            return result;

        }

        public async Task<int> RegisterUser(Users users)
        {
            var result = 0;
            var foundUser = await _userManager.FindByNameAsync(users.Name);
            if (foundUser != null)
            {
                return 0;
            }

            var user = new IdentityUser
            {
                UserName = users.Name
            };


            var resultt = await _userManager.CreateAsync(user, users.Password);
            if (resultt.Succeeded)
            {
                // установка куки
                //await userManager (user, false);
                var register = await _userManager.CreateSecurityTokenAsync(user);
                result = 1;
            }
            else
            {
                result = 0;
            }
            return result;
        }

        public async Task<bool> IsValidUserAsyncForRegister(Users users)
        {
            var u = _userManager.Users.FirstOrDefault(o => o.UserName == users.Name);
            var result = await _userManager.CheckPasswordAsync(u, users.Password);
            return !result;
        }
    }

}
