using AutoMapper;
using BackOffice.Application.Contracts;
using BackOffice.Application.Dtos.Auth;
using BackOffice.Application.Dtos;
using BackOffice.Application.Models;
using BackOffice.Domain.Entities.Users;
using BackOffice.Persistance.Identity.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackOffice.Persistance.Identity
{
    public class UserService : IUserService
    {
        private readonly AppUserManager _userManager;
        private readonly AppSignInManager _signInManager;
       // private readonly IUnitOfWork<TicketingDbContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITokenInfoService _tokenService;


        public UserService(AppUserManager userManager
            , AppSignInManager signInManager
            , IMapper mapper
            , ITokenInfoService tokenInfoService)
        //, IUnitOfWork<TicketingDbContext> unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenService = tokenInfoService;
            //_unitOfWork = unitOfWork;
        }

        public async Task<OperationResult<CreateResultDto>> Create(UserCreateDto user, string role)
        {


            var userToAdd = _mapper.Map<User>(user);
            userToAdd.SecurityStamp = Guid.NewGuid().ToString();

            var createResult = await _userManager.CreateAsync(userToAdd, user.Password);

            if (!createResult.Succeeded)
                return OperationResult<CreateResultDto>.FailureResult(createResult.Errors.Aggregate("خطا:", (str, p) => str += p.Description + ",",
                     str => str.Substring(0, str.Length - 1)));


            var addRoleResult = await _userManager.AddToRoleAsync(userToAdd, role);
            if (!addRoleResult.Succeeded)
                return OperationResult<CreateResultDto>.FailureResult(addRoleResult.Errors.Aggregate("خطا:", (str, p) => str += p.Description + ",",
                     str => str.Substring(0, str.Length - 1)));

            return OperationResult<CreateResultDto>.SuccessResult(new CreateResultDto { Id = userToAdd.Id });
        }

        public async Task<OperationResult<bool>> Delete(int id)
        {
            var userToDelete = await _userManager.Users
                .SingleOrDefaultAsync(u => u.Id == id);
            if (userToDelete is null)
                return OperationResult<bool>.FailureResult("کاربر پیدا نشد");



            var deleteResult = await _userManager.DeleteAsync(userToDelete);
            if (!deleteResult.Succeeded)
                return OperationResult<bool>.FailureResult(deleteResult.Errors.Aggregate("خطا:", (str, p) => str += p.Description + ",",
                     str => str.Substring(0, str.Length - 1)));

            return OperationResult<bool>.SuccessResult(true);
        }

        public async Task<OperationResult<List<UserListDto>>> GetAll()
        {


            var result = await _mapper.ProjectTo<UserListDto>(_userManager.Users.AsNoTracking()).ToListAsync();

            return OperationResult<List<UserListDto>>.SuccessResult(result);
        }

        public async Task<OperationResult<UserDto>> GetById(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
                return OperationResult<UserDto>.NotFoundResult("کاربر مورد نظر پیدا نشد.");

            var response = _mapper.Map<UserDto>(user);
            var userRoles = await _userManager.GetRolesAsync(user);
            response.Roles = userRoles.Aggregate((s, p) => s + "," + p);
            return OperationResult<UserDto>.SuccessResult(response);
        }



        public async Task<OperationResult<TokenDto>> GetRefreshToken(string refreshToken)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user is null)
                return OperationResult<TokenDto>.NotFoundResult("توکن ورودی اشتباه است.");

            var token = await _tokenService.GenerateToken(user);

            user.RefreshToken = token.RefreshToken;
            //await _unitOfWork.Commit();
            //TODO Adding UiOfWork

            return OperationResult<TokenDto>.SuccessResult(token);
        }

        public async Task<OperationResult<TokenDto>> GetToken(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user is null)
                return OperationResult<TokenDto>.FailureResult("کاربر یافت نشد");

            if (user.LockoutEnabled)
                return OperationResult<TokenDto>.FailureResult("کاربر قفل شده است ، جهت رفع مشکل با ادمین سیستم تماس حاصل نمایید.");

            var passwordValidator = await _signInManager.PasswordSignInAsync(user, password, true, false).ConfigureAwait(false);

            if (!passwordValidator.Succeeded)
                return OperationResult<TokenDto>.FailureResult("اطلاعات وارد شده اشتباه است");


            var token = await _tokenService.GenerateToken(user).ConfigureAwait(false);
            return OperationResult<TokenDto>.SuccessResult(token);

        }

        public async Task<OperationResult<bool>> Update(UserUpdateDto model)
        {

            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            if (user is null)
                return OperationResult<bool>.NotFoundResult("کاربر مورد نظر پیدا نشد");

            _mapper.Map(model, user);

            await _userManager.UpdateSecurityStampAsync(user);

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                var tokenforpassword = await _userManager.GeneratePasswordResetTokenAsync(user);
                var Password = await _userManager.ResetPasswordAsync(user, tokenforpassword, model.Password);
                user.SecurityStamp = Guid.NewGuid().ToString();

            }
            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
                return OperationResult<bool>.FailureResult(updateResult.Errors.Aggregate("خطا:", (str, p) => str += p.Description + ",",
                     str => str.Substring(0, str.Length - 1)));

            return OperationResult<bool>.SuccessResult(true);
        }
    }

}
