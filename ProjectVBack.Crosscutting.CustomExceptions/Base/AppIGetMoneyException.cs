using System.Runtime.Serialization;

namespace ProjectVBack.Crosscutting.CustomExceptions;

[Serializable]
public class AppIGetMoneyException : Exception
{
    public AppIGetMoneyException() : base() { }

    public AppIGetMoneyException(string message) : base(message) { }

    public AppIGetMoneyException(string? message, Exception? innerException) : base(message, innerException) { }

    protected AppIGetMoneyException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    public override void GetObjectData(SerializationInfo info, StreamingContext context) => base.GetObjectData(info, context);
}
