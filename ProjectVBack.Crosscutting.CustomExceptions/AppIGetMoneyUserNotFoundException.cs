using ProjectVBack.Crosscutting.Utils.Errors;
using System.Runtime.Serialization;

namespace ProjectVBack.Crosscutting.CustomExceptions;

[Serializable]
public class AppIGetMoneyUserNotFoundException : AppIGetMoneyException
{
    public AppIGetMoneyUserNotFoundException() : base(ExceptionMessagesTexts.UserNotFoundError) { }

    public AppIGetMoneyUserNotFoundException(string message) : base(message) { }

    public AppIGetMoneyUserNotFoundException(string? message, Exception? inner) : base(message, inner) { }

    protected AppIGetMoneyUserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override void GetObjectData(SerializationInfo info, StreamingContext context) => base.GetObjectData(info, context);
}
