using ProjectVBack.Crosscutting.CustomExceptions;

public class AppIGetMoneyCategroyNotFoundException : AppIGetMoneyException
{
    public AppIGetMoneyCategroyNotFoundException() : base("Category not found") { }

    public AppIGetMoneyCategroyNotFoundException(string message) : base(message) { }

    public AppIGetMoneyCategroyNotFoundException(string message, Exception inner) : base(message, inner) { }
}

