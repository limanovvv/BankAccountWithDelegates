using System.Text.Json;
using BankAccountWithDelegates.CreatedExceptions;

namespace BankAccountWithDelegates.BA;

public class ProgramBA
{
    public static void Execute()
    {
        BankAccountManager manager = null;

        try
        {
            manager = new BankAccountManager();
        }
        catch (IOException ioException)
        {
            Console.WriteLine(ioException.Message);
        }
        catch (JsonException jsonException)
        {
            Console.WriteLine(jsonException.Message);
        }

        long accNumber = 0;
        var choise = ' ';
        var flag = false;
        short currentTry = 0;

        do
        {
            try
            {
                Console.WriteLine("Вы являетесь нашим клиентом? y - да n - нет");
                choise = char.ToLower(char.Parse(Console.ReadLine()));

                flag = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
            }
        } while (!flag);

        if (choise == 'n')
        {
            Console.WriteLine("Создадим счет, Введите ваше Имя");
            accNumber = manager.CreateAccount(Console.ReadLine(), 0);
        }
        else
        {
            while (currentTry <= 3)
                try
                {
                    Console.WriteLine("Введите номер счета");
                    accNumber = manager.GetAccount(long.Parse(Console.ReadLine())).AccountNumber;
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Такого счета не существует");
                    currentTry++;
                }

            if (currentTry == 3)
            {
                return;
            }
        }

        bool correctOperation = false;
        short operation = 0;


        do
        {
            
            do
            {
                try
                {
                    Console.WriteLine("Выберите операцию: 1 - Deposit 2 - Withdraw, 3 - Transfer, 4 - Exit");

                    operation = short.Parse(Console.ReadLine());
                    correctOperation = true;

                }
                catch (Exception)
                {
                    Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                }
            } while (!correctOperation);

            switch (operation)
            {
                case 1:

                    try
                    {
                        Console.WriteLine("сумма пополнения");
                        manager.Deposit(accNumber, short.Parse(Console.ReadLine()));
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Некорректный ввод cуммы пополнения");
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
                        manager.Withdraw(accNumber, short.Parse(Console.ReadLine()));
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Некорректный ввод cуммы");
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

                    try
                    {
                        Console.WriteLine("сумма");
                        manager.Transfer(accNumber, secondAccountNumber, short.Parse(Console.ReadLine()));
                    }
                    catch (AccountNumberException accountNumberException)
                    {
                        Console.WriteLine(accountNumberException.Message);
                    }

                    manager.PrintBalance(accNumber);
                    manager.PrintBalance(secondAccountNumber);

                    break;
                case 4:
                    return;
                default:

                    Console.WriteLine("Неправильно выбрана операция 1 - Deposit 2 - Withdraw 3 - Transfer 4 - Exit");

                    break;
            }
        } while (true);
    }
}
