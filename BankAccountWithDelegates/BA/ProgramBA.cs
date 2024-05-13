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
            long accNumber;

            Console.WriteLine("Вы являетесь нашим клиентом? y - да n - нет");
            var choise = char.ToLower(char.Parse(Console.ReadLine()));

            if (choise == 'n')
            {
                Console.WriteLine("Создадим счет, Введите ваше Имя");
                accNumber = manager.CreateAccount(Console.ReadLine(), 0);
            }
            else
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Введите номер счета");
                        accNumber = manager.GetAccount(long.Parse(Console.ReadLine())).AccountNumber;
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Такого счета не существует");

                    }
                }
            }


            do
            {
                    
                Console.WriteLine("Выберите операцию: 1 - Deposit 2 - Withdraw, 3 - Transfer, 4 - exit");

                    var operation = Int16.Parse(Console.ReadLine());

                    switch (operation)
                    {
                        case 1:
                    
                            try
                            {
                            Console.WriteLine("сумма пополнения");
                            manager.Deposit(accNumber, Int16.Parse(Console.ReadLine()));
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
                    
                            manager.PrintBalance(accNumber);
                    
                            break;
                        case 2:
                    
                            try
                            {
                            Console.WriteLine("сумма");
                            manager.Withdraw(accNumber, Int16.Parse(Console.ReadLine()));
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
                    
                            manager.PrintBalance(accNumber);
                    
                            break;
                        case 3:

                        long secondAccountNumber;

                        while (true)
                        {
                            try
                            {
                                Console.WriteLine("Введите номер счета");
                                secondAccountNumber = manager.GetAccount(long.Parse(Console.ReadLine())).AccountNumber;
                                break;
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Такого счета не существует");

                            }
                        }

                        try
                            {
                            Console.WriteLine("сумма");
                            manager.Transfer(accNumber, secondAccountNumber, Int16.Parse(Console.ReadLine()));

                            }
                            catch (AccountNumberException accountNumberException)
                            {
                                Console.WriteLine(accountNumberException.Message);
                            }
                    
                            manager.PrintBalance(accNumber);
                            manager.PrintBalance(secondAccountNumber);
                    
                            break;
                    case 4:
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
