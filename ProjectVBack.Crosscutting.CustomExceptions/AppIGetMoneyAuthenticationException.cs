using ProjectVBack.Crosscutting.Utils.Errors;
using System.Runtime.Serialization;

namespace ProjectVBack.Crosscutting.CustomExceptions;

[Serializable]
public class AppIGetMoneyAuthenticationException : AppIGetMoneyException
{
    public AppIGetMoneyAuthenticationException() : base(ExceptionMessagesTexts.AuthenticateError) { }

    public AppIGetMoneyAuthenticationException(string message) : base(message) { }

    public AppIGetMoneyAuthenticationException(string? message, Exception? inner) : base(message, inner) { }

    protected AppIGetMoneyAuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override void GetObjectData(SerializationInfo info, StreamingContext context) => base.GetObjectData(info, context);
}

