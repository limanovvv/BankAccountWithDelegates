using BankAccountWithDelegates.CreatedExceptions;

namespace BankAccountWithDelegates.BA;

class ProgramBA
{
    public static void Execute()
    {
        BankAccountManager manager = new BankAccountManager();

        manager.CreateAccount("Alice", 1000);
        manager.CreateAccount("Mark", 1000);

        long firstAccountNumber = manager.accounts[0].AccountNumber;
        long secondAccountNumber = manager.accounts[1].AccountNumber;
        
        manager.Deposit(firstAccountNumber, 500);
        try
        {
            manager.Withdraw(firstAccountNumber, 200);
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
        
        manager.Transfer(firstAccountNumber, secondAccountNumber, 300);

        manager.PrintBalance(firstAccountNumber);
        manager.PrintBalance(secondAccountNumber);
    }
}
