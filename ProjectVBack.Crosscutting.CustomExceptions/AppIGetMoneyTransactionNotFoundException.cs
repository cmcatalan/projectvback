using ProjectVBack.Crosscutting.Utils.Errors;
using System.Runtime.Serialization;

namespace ProjectVBack.Crosscutting.CustomExceptions;

[Serializable]
public class AppIGetMoneyTransactionNotFoundException : AppIGetMoneyException
{
    public AppIGetMoneyTransactionNotFoundException() : base(ExceptionMessagesTexts.TransactionNotFoundError) { }

    public AppIGetMoneyTransactionNotFoundException(string message) : base(message) { }

    public AppIGetMoneyTransactionNotFoundException(string? message, Exception? inner) : base(message, inner) { }

    protected AppIGetMoneyTransactionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override void GetObjectData(SerializationInfo info, StreamingContext context) => base.GetObjectData(info, context);
}

