using BackOffice.Application.Dtos.Auth;
using BackOffice.Domain.Entities.Users;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Contracts
{
    public interface ITokenInfoService
    {
        Task<TokenDto> GenerateToken(User user);
        long GetCurrentUserId();
        TokenValidationParameters GetValidationParameters();
    }
}
