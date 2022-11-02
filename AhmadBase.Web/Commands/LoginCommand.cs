using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AhmadBase.Core.interfere.IReposetory;
using AhmadBase.Core.Types;
using AhmadBase.Inferastracter;
using AhmadBase.Inferastracter.Datas.Entities;
using MediatR;

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



            
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1gfbfghdfghfghgfh ghghhfghdfhfghdgfhfghghdgfhdgfh23*/"));


            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);



            var token = new JwtSecurityToken("Ahmad.Com", "Ahmad.Com", expires: DateTime.Today.AddDays(5),
                claims: new List<Claim>()
                {
                    new Claim("userId","1"),
                    new Claim("role","Admin")
                }, signingCredentials: cred);




            return Ok(new JwtSecurityTokenHandler().WriteToken(token));

        }
    }
}
