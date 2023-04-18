using BackOffice.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Domain.Entities.Users
{
    public class Role: IdentityRole<int>, IEntity
    {
    }
}
