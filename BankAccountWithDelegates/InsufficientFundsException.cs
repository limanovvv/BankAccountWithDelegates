namespace BankAccountWithDelegates;

public class InsufficientFundsException : Exception
{
    public double Sum;

    public InsufficientFundsException(string message, double sum) : base(message)
    {
        Sum = sum;
    }
}