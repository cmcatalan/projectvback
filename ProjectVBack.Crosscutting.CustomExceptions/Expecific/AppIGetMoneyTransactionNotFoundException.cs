using ProjectVBack.Crosscutting.CustomExceptions;

public class AppIGetMoneyTransactionNotFoundException : AppIGetMoneyException
{
    public AppIGetMoneyTransactionNotFoundException() : base("Transaction not found") { }

    public AppIGetMoneyTransactionNotFoundException(string message) : base(message) { }

    public AppIGetMoneyTransactionNotFoundException(string message, Exception inner) : base(message, inner) { }
}

