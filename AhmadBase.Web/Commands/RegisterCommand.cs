using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AhmadBase.Core.Dtos;
using AhmadBase.Core.interfere.IReposetory;
using AhmadBase.Core.Types;
using AhmadBase.Inferastracter;
using AhmadBase.Inferastracter.Datas.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AhmadBase.Web.Commands
{
    public class RegisterCommand : IRequest<ServiceResult<string>>
    {
        public string Email { get; set; }
        public string PassWord { get; set; }
        public string PassWordRepeat { get; set; }
        public string Phone { get; set; }

    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ServiceResult<string>>
    {
        private readonly IUnitOfWork<AppDbContext> unitOfWork;
        private readonly IAES AES;
        public RegisterCommandHandler(IUnitOfWork<AppDbContext> unitOfWork, IAES aES)
        {
            this.unitOfWork = unitOfWork;
            this.AES = aES;
        }
        public async Task<ServiceResult<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var repo = unitOfWork.GetRepository<UserEntity>();
 
            
            
            var userExist = repo.GetFirstOrDefault(predicate: x => x.Email.Equals(request.Email));

            if (userExist != null)
                return ServiceResult.Empty.SetError("InvalidData").To<string>();

  
            if (request.PassWord != request.PassWordRepeat )
                return ServiceResult.Empty.SetError("InvalidData").To<string>();


            var passWordHash = AES.Encrypt(request.PassWord);
            
            var user = new UserEntity(request.Email,passWordHash,passWordHash,request.Phone);

               await repo.InsertAsync(user);

            await  unitOfWork.SaveChangesAsync();
            return ServiceResult.Create<string>(user.Id.ToString());
        }
    }
}
