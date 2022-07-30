using Message.Shared;

namespace Message.Api.Messages;

public record UserCreated(string Name, string Email, string Otp) :  IMessage;