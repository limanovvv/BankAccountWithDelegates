namespace BankAccountWithDelegates;

class Program
{
    static void Main()
    {
        BankAccountManager manager = new BankAccountManager();

        manager.CreateAccount("Alice", 1000);
        manager.CreateAccount("Mark", 1000);
        manager.Deposit(1, 500);
        try
        {
            manager.Withdraw(1, 200);
        }
        catch (AccountNumberException accountNumberException)
        {
            Console.WriteLine(accountNumberException.Message);
        }
        catch (InsufficientFundsException insufficientFundsException)
        {
            Console.WriteLine(insufficientFundsException.Message);
            Console.WriteLine($"Ваш баланс:  {insufficientFundsException.Sum}");
        }
        
        manager.Transfer(1, 2, 300);

        manager.PrintBalance(1);
        manager.PrintBalance(2);
    }
}
