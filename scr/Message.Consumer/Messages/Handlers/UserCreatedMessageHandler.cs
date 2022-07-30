using Message.Consumer.DAL;
using Message.Consumer.DAL.Models;
using Message.Shared;

namespace Message.Consumer.Messages.Handlers;

internal sealed class UserCreatedMessageHandler : IMessageHandler<UserCreatedMessage>
{
    private readonly NotifyDbContext _dbContext;

    public UserCreatedMessageHandler(NotifyDbContext dbContext)
        => _dbContext = dbContext;

    public async Task HandleAsync(UserCreatedMessage message)
    {
        var userCreated = new UserCreatedModel
        {
            Id = Guid.NewGuid(),
            Email = message.Email,
            Name = message.Name,
            Otp = message.Otp,
            IsSendEmail = false,
        };

        if (message.Email.Any())
        {
            await _dbContext.UserCreated.AddAsync(userCreated);
        }
    }
}