using Application.CQRS.Notifications;
using Infrastructure.Models;
using Infrastructure.Utility;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Application.CQRS.AuthenticateCommandQuery.Command;
public class GenerateNewTokenCommand : IRequest<GenerateNewTokenCommandResponse>
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}

public class GenerateNewTokenCommandResponse
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}

public class GenerateNewTokenCommandHandler : IRequestHandler<GenerateNewTokenCommand, GenerateNewTokenCommandResponse>
{
    private readonly OnlineShopDbContext onlineShopDbContext;
    private readonly EncryptionUtility encryptionUtility;
    private readonly IMediator mediator;
    private readonly Configs configs;

    public GenerateNewTokenCommandHandler(OnlineShopDbContext onlineShopDbContext,
     EncryptionUtility encryptionUtility, IOptions<Configs> options,
     IMediator mediator)
    {
        this.onlineShopDbContext = onlineShopDbContext;
        this.encryptionUtility = encryptionUtility;
        this.mediator = mediator;
        this.configs = options.Value;
    }
    public async Task<GenerateNewTokenCommandResponse> Handle(GenerateNewTokenCommand request, CancellationToken cancellationToken)
    {
        var userRefreshToken = await onlineShopDbContext.UserRefreshTokens
        .SingleOrDefaultAsync(q => q.RefreshToken == request.RefreshToken);

        if (userRefreshToken == null) throw new Exception();

        var token = encryptionUtility.GetNewToken(userRefreshToken.UserId);
        var refreshToken = encryptionUtility.GetNewRefreshToken();

        //1-insert or update refresh token in db
        //2-send login sms
        var addRefreshTokenNotification = new AddRefreshTokenNotification
        {
            RefreshToken = refreshToken,
            RefreshTokenTimeOut = configs.RefreshTokenTimeout, //read from configs
            UserId = userRefreshToken.UserId,
        };
        await mediator.Publish(addRefreshTokenNotification);

        var response = new GenerateNewTokenCommandResponse{
            RefreshToken = refreshToken,
            Token = token
        };

        return response;
    }
}