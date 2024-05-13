namespace BankAccountWithDelegates.Calculator;

public static class CalculatorProgram
{
    public static void Execute()
    {
        double number1;
        char operation;
        double number2;
        var flag = true;

        while (flag)
        {
            Console.WriteLine("Введите первое число:");
            BankAccountLogger.LogInfo("Калькулятор. Просим ввести первое число");

            try
            {
                BankAccountLogger.LogInfo("Калькулятор. Парсим первое число");
                number1 = double.Parse(Console.ReadLine());
                
            }
            catch (Exception e)
            {
                BankAccountLogger.LogError("ошибка при парсинге первого числа");
                throw;
            }

            Console.WriteLine("Выберите операцию + - * / "); 
            BankAccountLogger.LogInfo("Калькулятор. Просим ввести операцию");


            try
            {
                BankAccountLogger.LogInfo("Калькулятор. Парсим операцию");
                operation = char.Parse(Console.ReadLine());
                
            }
            catch (Exception e)
            {
                BankAccountLogger.LogError("ошибка при парсинге операции");
                throw;
            }

            Console.WriteLine("Введите второе число:");
            BankAccountLogger.LogInfo("Калькулятор. Просим ввести второе число");

            try
            {
                BankAccountLogger.LogInfo("Калькулятор. Парсим второе число");
                number2 = double.Parse(Console.ReadLine());
                
            }
            catch (Exception e)
            {
                BankAccountLogger.LogError("ошибка при парсинге второго числа");
                throw;
            }

            switch (operation)
            {
                case '+':

                    CalculatorWithDelegates.Do(number1, number2, CalculatorWithDelegates.Add);
                    BankAccountLogger.LogInfo("Калькулятор. Складываем");

                    break;
                case '-':

                    CalculatorWithDelegates.Do(number1, number2, CalculatorWithDelegates.Substract);
                    BankAccountLogger.LogInfo("Калькулятор. Вычитаем");

                    break;
                case '*':

                    CalculatorWithDelegates.Do(number1, number2, CalculatorWithDelegates.Multiply);
                    BankAccountLogger.LogInfo("Калькулятор. Умножаем");

                    break;
                case '/':

                    try
                    {
                        CalculatorWithDelegates.Do(number1, number2, CalculatorWithDelegates.Divide);
                        BankAccountLogger.LogInfo("Калькулятор. Делим");
                    }
                    catch (DivideByZeroException e)
                    {
                        BankAccountLogger.LogError("Калькулятор. Ошибка. Нельзя делить на нуль");
                        Console.WriteLine(e);
                    }

                    break;
                default:

                    Console.WriteLine("Операция была введена неккоректно + - * / ");
                    BankAccountLogger.LogInfo("Калькулятор. Операция была введена неккоректно");

                    break;
            }
            
            Console.WriteLine("Желаете продолжить калькулятор? y - да n - нет");
            var choise = char.ToLower(char.Parse(Console.ReadLine()));

            if (choise == 'y') flag = true;
            else flag = false;
            
        }
    }
}