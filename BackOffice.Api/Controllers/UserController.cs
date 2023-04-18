using BackOffice.Application.Contracts;
using BackOffice.Application.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackOffice.Api.Controllers
{
    [ApiController]
    [Route("api/v1/User")]
    [Authorize(Roles = "admin")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await _userService.GetToken(username, password);
            return OperationResult(result);
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var result = await _userService.GetRefreshToken(refreshToken);
            return OperationResult(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAll();
            return OperationResult(result);
        }

        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetById(id);
            return OperationResult(result);
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserCreateDto model)
        {
            var result = await _userService.Create(model, "user");
            return OperationResult(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterAdminUser(UserCreateDto model)
        {
            var result = await _userService.Create(model, "admin");
            return OperationResult(result);
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> Update(UserUpdateDto model)
        {
            var result = await _userService.Update(model);
            return OperationResult(result);
        }
        [HttpDelete("[action]/{userId:int}")]
        public async Task<IActionResult> Delete(int userId)
        {
            var result = await _userService.Delete(userId);
            return OperationResult(result);
        }


    }

}
