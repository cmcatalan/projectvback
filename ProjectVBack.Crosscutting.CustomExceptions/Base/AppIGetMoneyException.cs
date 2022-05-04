namespace ProjectVBack.Crosscutting.CustomExceptions;

public class AppIGetMoneyException : Exception
{
    public AppIGetMoneyException() : base() { }

    public AppIGetMoneyException(string message) : base(message) { }

    public AppIGetMoneyException(string message, Exception inner) : base(message, inner) { }
}
