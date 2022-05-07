using ProjectVBack.Crosscutting.CustomExceptions;

public class AppIGetMoneyUserCreationException : AppIGetMoneyException
{
    public AppIGetMoneyUserCreationException() : base("Category can't be created") { }

    public AppIGetMoneyUserCreationException(string message) : base(message) { }

    public AppIGetMoneyUserCreationException(string message, Exception inner) : base(message, inner) { }
}

