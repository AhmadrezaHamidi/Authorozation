using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AhmadBase.Core.interfere.IReposetory;
using AhmadBase.Core.Types;
using AhmadBase.Inferastracter;
using AhmadBase.Inferastracter.Datas.Entities;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace AhmadBase.Web.Commands
{
    public class LoginCommand : IRequest<ServiceResult<string>>
    {
        public string Email { get; set; }
        public string PassWord { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<RegisterCommand, ServiceResult<string>>
    {
        private readonly IUnitOfWork<AppDbContext> unitOfWork;
        private readonly IAES AES;
        public LoginCommandHandler(IUnitOfWork<AppDbContext> unitOfWork, IAES aES)
        {
            this.unitOfWork = unitOfWork;
            this.AES = aES;
        }

        public async Task<ServiceResult<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            var repo = unitOfWork.GetRepository<UserEntity>();

            var userExist = repo.GetFirstOrDefault(predicate: x => x.Email.Equals(request.Email));

             
            if (userExist == null)
                return ServiceResult.Empty.SetError("This User Is Not Exist").To<string>();


            if (userExist.PassWordHash != request.PassWordRepeat)
                return ServiceResult.Empty.SetError("This User Is Not Exist").To<string>();



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1gfbfghdfghfghgfh ghghhfghdfhfghdgfhfghghdgfhdgfh23*/"));


            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



            var token = new JwtSecurityToken("Ahmad.Com", "Ahmad.Com", expires: DateTime.Today.AddDays(5),
                claims: new List<Claim>()
                {
                    new Claim("userId",userExist.Id.ToString()),
                    new Claim("role","Admin")
                }, signingCredentials: cred);



            return ServiceResult.Create<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
