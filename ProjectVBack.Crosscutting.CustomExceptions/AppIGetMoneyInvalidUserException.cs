using System.Runtime.Serialization;

namespace ProjectVBack.Crosscutting.CustomExceptions;

[Serializable]
public class AppIGetMoneyInvalidUserException : AppIGetMoneyException
{
    public AppIGetMoneyInvalidUserException() : base("Invalid user") { }

    public AppIGetMoneyInvalidUserException(string message) : base(message) { }

    public AppIGetMoneyInvalidUserException(string? message, Exception? inner) : base(message, inner) { }

    protected AppIGetMoneyInvalidUserException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override void GetObjectData(SerializationInfo info, StreamingContext context) => base.GetObjectData(info, context);
}
