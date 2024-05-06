namespace BankAccountWithDelegates.CreatedExceptions;

public class DepositException: Exception
{
    public double Sum;

    public DepositException(string message, double sum) : base(message)
    {
        Sum = sum;
    }
}