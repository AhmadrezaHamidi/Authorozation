using BackOffice.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace BackOffice.Domain.Entities.Users
{
    public class UserLogin : IdentityUserLogin<int>, IEntity
    {
    }
}
