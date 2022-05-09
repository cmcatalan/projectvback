using ProjectVBack.Crosscutting.Utils.Errors;
using System.Runtime.Serialization;

namespace ProjectVBack.Crosscutting.CustomExceptions;

[Serializable]
public class AppIGetMoneyInvalidTransactionException : AppIGetMoneyException
{
    public AppIGetMoneyInvalidTransactionException() : base(ExceptionMessagesTexts.InvalidTransactionError) { }

    public AppIGetMoneyInvalidTransactionException(string message) : base(message) { }

    public AppIGetMoneyInvalidTransactionException(string? message, Exception? inner) : base(message, inner) { }

    protected AppIGetMoneyInvalidTransactionException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override void GetObjectData(SerializationInfo info, StreamingContext context) => base.GetObjectData(info, context);
}
