using ProjectVBack.Crosscutting.Utils.Errors;
using System.Runtime.Serialization;

namespace ProjectVBack.Crosscutting.CustomExceptions;

[Serializable]
public class AppIGetMoneyCategroyNotFoundException : AppIGetMoneyException
{
    public AppIGetMoneyCategroyNotFoundException() : base(ExceptionMessagesTexts.CategoryNotFoundError) { }

    public AppIGetMoneyCategroyNotFoundException(string message) : base(message) { }

    public AppIGetMoneyCategroyNotFoundException(string? message, Exception? inner) : base(message, inner) { }

    protected AppIGetMoneyCategroyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override void GetObjectData(SerializationInfo info, StreamingContext context) => base.GetObjectData(info, context);
}

