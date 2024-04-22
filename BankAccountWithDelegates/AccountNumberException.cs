namespace BankAccountWithDelegates;

public class AccountNumberException : Exception
{ 
    public AccountNumberException(string message) : base(message) {}
}