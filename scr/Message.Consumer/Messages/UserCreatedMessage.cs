using Message.Shared;

namespace Message.Consumer.Messages;

public record UserCreatedMessage(string Name, string Email, string Otp) :  IMessage;