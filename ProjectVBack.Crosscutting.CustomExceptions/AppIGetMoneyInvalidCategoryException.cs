using System.Runtime.Serialization;

namespace ProjectVBack.Crosscutting.CustomExceptions;

[Serializable]
public class AppIGetMoneyInvalidCategoryException : AppIGetMoneyException
{
    public AppIGetMoneyInvalidCategoryException() : base("Invalid category") { }

    public AppIGetMoneyInvalidCategoryException(string message) : base(message) { }

    public AppIGetMoneyInvalidCategoryException(string? message, Exception? inner) : base(message, inner) { }

    protected AppIGetMoneyInvalidCategoryException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override void GetObjectData(SerializationInfo info, StreamingContext context) => base.GetObjectData(info, context);
}
