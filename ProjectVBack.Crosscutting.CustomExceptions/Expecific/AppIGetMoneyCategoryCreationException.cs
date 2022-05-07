using ProjectVBack.Crosscutting.CustomExceptions;

public class AppIGetMoneyCategoryCreationException : AppIGetMoneyException
{
    public AppIGetMoneyCategoryCreationException() : base("Category can't be created") { }

    public AppIGetMoneyCategoryCreationException(string message) : base(message) { }

    public AppIGetMoneyCategoryCreationException(string message, Exception inner) : base(message, inner) { }
}

