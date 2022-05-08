using ProjectVBack.Crosscutting.CustomExceptions;

public class AppIGetMoneyAuthenticationException : AppIGetMoneyException
{
    public AppIGetMoneyAuthenticationException() : base("Category not found") { }

    public AppIGetMoneyAuthenticationException(string message) : base(message) { }

    public AppIGetMoneyAuthenticationException(string message, Exception inner) : base(message, inner) { }
}

