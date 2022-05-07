using System.Runtime.Serialization;

namespace ProjectVBack.Crosscutting.CustomExceptions;

[Serializable]
public class AppIGetMoneyCategoryCreationException : AppIGetMoneyException
{
    public AppIGetMoneyCategoryCreationException() : base("Category can't be created") { }

    public AppIGetMoneyCategoryCreationException(string message) : base(message) { }

    public AppIGetMoneyCategoryCreationException(string? message, Exception? inner) : base(message, inner) { }

    protected AppIGetMoneyCategoryCreationException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override void GetObjectData(SerializationInfo info, StreamingContext context) => base.GetObjectData(info, context);
}

