using System.Text.Json;
using BankAccountWithDelegates.CreatedExceptions;

namespace BankAccountWithDelegates.BA;

class ProgramBA
{
    public void Execute()
    {
        try
        {

            BankAccountManager manager = new BankAccountManager();

            manager.CreateAccount("Alice", 1000);
            manager.CreateAccount("Mark", 1000);

            long firstAccountNumber = BankAccountManager.accounts[0].AccountNumber;
            long secondAccountNumber = BankAccountManager.accounts[1].AccountNumber;
            
            
            

            try
            {
                manager.Deposit(firstAccountNumber, 500);
            }
            catch (AccountNumberException accountNumberException)
            {
                Console.WriteLine(accountNumberException.Message);
            }
            catch (JsonException jsonException)
            {
                Console.WriteLine(jsonException.Message);
            }
            catch (IOException ioException)
            {
                Console.WriteLine(ioException.Message);
            }

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
            catch (JsonException jsonException)
            {
                Console.WriteLine(jsonException.Message);
            }
            catch (IOException ioException)
            {
                Console.WriteLine(ioException.Message);
            }

            try
            {
                manager.Transfer(firstAccountNumber, secondAccountNumber, 300);

            }
            catch (AccountNumberException accountNumberException)
            {
                Console.WriteLine(accountNumberException.Message);
            }


            manager.PrintBalance(firstAccountNumber);
            manager.PrintBalance(secondAccountNumber);
        }
        catch (IOException ioException)
        {
            Console.WriteLine(ioException.Message);
        }
        catch (JsonException jsonException)
        {
            Console.WriteLine(jsonException.Message);
        }

    }
}
