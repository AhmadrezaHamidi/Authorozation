using BackOffice.Application.Profiles;
using BackOffice.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackOffice.Application.Dtos.Auth
{
    public class UserDto : ICreateMapper<User>
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Roles { get; set; }

    }
    public class UserListDto : ICreateMapper<User>
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


    }
}
