using BackOffice.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace BackOffice.Domain.Entities.Users
{
    public class UserClaim : IdentityUserClaim<int>, IEntity
    {
    }
}
