using Application.CQRS.Notifications;
using Infrastructure.Models;
using Infrastructure.Utility;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.AuthenticateCommandQuery.Command
{
    public class LoginCommand : IRequest<LoginCommandResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandResponse
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public int ExpireTime { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
    {
        private readonly OnlineShopDbContext onlineShopDbContext;
        private readonly EncryptionUtility encryptionUtility;
        private readonly IMediator mediator;
        private readonly Configs configs;
 
        public LoginCommandHandler(OnlineShopDbContext onlineShopDbContext,
         EncryptionUtility encryptionUtility, IMediator mediator,
         IOptions<Configs> options)
        {
            this.onlineShopDbContext = onlineShopDbContext;
            this.encryptionUtility = encryptionUtility;
            this.mediator = mediator;
            this.configs = options.Value;
        }
        public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await onlineShopDbContext.Users.SingleOrDefaultAsync(q => q.UserName == request.UserName);
            if (user == null) throw new Exception();

            var hashPassowrd = encryptionUtility.GetSHA256(request.Password, user.PasswordSalt);
            if (user.Password != hashPassowrd) throw new Exception();

            var token = encryptionUtility.GetNewToken(user.Id);
            var refreshToken = encryptionUtility.GetNewRefreshToken();

            //1-insert or update refresh token in db
            //2-send login sms
            var addRefreshTokenNotification = new AddRefreshTokenNotification
            {
                RefreshToken = refreshToken,
                RefreshTokenTimeOut = configs.RefreshTokenTimeout, //read from configs
                UserId = user.Id,
            };
            await mediator.Publish(addRefreshTokenNotification);

            var response = new LoginCommandResponse
            {
                UserName = user.UserName,
                Token = token,
                RefreshToken = refreshToken,
            };
            return response;
        }
    }
}
