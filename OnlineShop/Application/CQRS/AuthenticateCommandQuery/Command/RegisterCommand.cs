using Core.Entities;
using Infrastructure.Utility;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AuthenticateCommandQuery.Command
{
    public class RegisterCommand:IRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly OnlineShopDbContext onlineShopDbContext;
        private readonly EncryptionUtility encryptionUtility;

        public RegisterCommandHandler(OnlineShopDbContext onlineShopDbContext, EncryptionUtility encryptionUtility)
        {
            this.onlineShopDbContext = onlineShopDbContext;
            this.encryptionUtility = encryptionUtility;
        }
        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var salt = encryptionUtility.GetNewSalt();
            var hashPassowrd = encryptionUtility.GetSHA256(request.Password, salt);

            var user = new User
            {
                Id= Guid.NewGuid(),
                Password = hashPassowrd,
                PasswordSalt = salt,
                RegisterDate= DateTime.Now,
                UserName = request.UserName
            };

            await onlineShopDbContext.Users.AddAsync(user);
            await onlineShopDbContext.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
