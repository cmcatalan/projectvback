using ProjectVBack.Crosscutting.Utils.Errors;
using System.Runtime.Serialization;

namespace ProjectVBack.Crosscutting.CustomExceptions;

[Serializable]
public class AppIGetMoneyCategoryDefaultException : AppIGetMoneyException
{
    public AppIGetMoneyCategoryDefaultException() : base(ExceptionMessagesTexts.CategoryDefault) { }

    public AppIGetMoneyCategoryDefaultException(string message) : base(message) { }

    public AppIGetMoneyCategoryDefaultException(string? message, Exception? inner) : base(message, inner) { }

    protected AppIGetMoneyCategoryDefaultException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override void GetObjectData(SerializationInfo info, StreamingContext context) => base.GetObjectData(info, context);
}
