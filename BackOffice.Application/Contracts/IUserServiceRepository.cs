using BackOffice.Application.Dtos.Auth;

namespace BackOffice.Application.Contracts
{
    public interface IUserServiceRepository
    {
        Task<bool> IsValidUserAsyncForLogin(Users users);
        Task<bool> IsValidUserAsyncForRegister(Users users);
        Task<int> RegisterUser(Users users);
        UserRefreshTokens AddUserRefreshTokens(UserRefreshTokens user);
        UserRefreshTokens GetSavedRefreshTokens(string username, string refreshtoken);
        void DeleteUserRefreshTokens(string username, string refreshToken);

        int SaveCommit();
    }

}
