using System.Text.Json;
using BankAccountWithDelegates.CreatedExceptions;

namespace BankAccountWithDelegates.BA;

class ProgramBA
{
    public static void Execute()
    {
        bool flag = false;
        
            try
            {

                BankAccountManager manager = new BankAccountManager();

                manager.CreateAccount("Alice", 1000);
                manager.CreateAccount("Mark", 1000);

                long firstAccountNumber = BankAccountManager.accounts[0].AccountNumber;
                long secondAccountNumber = BankAccountManager.accounts[1].AccountNumber;

                do
                {
                    Console.WriteLine("Выберите операцию: 1 - Deposit 2 - Withdraw, 3 - Transfer");

                    var choise = Int16.Parse(Console.ReadLine());

                    switch (choise)
                    {
                    case 1:
                    
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
                    
                        manager.PrintBalance(firstAccountNumber);
                        flag = true;
                    
                        break;
                    case 2:
                    
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
                    
                        manager.PrintBalance(firstAccountNumber);
                        flag = true;
                    
                        break;
                    case 3:
                    
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
                        flag = true;
                    
                        break;
                    default:
                        
                        Console.WriteLine("Неправильно выбрана операция 1 - Deposit 2 - Withdraw, 3 - Transfer");
                        
                        break;
                    }
                } while (!flag);

                
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
