namespace BankAccountWithDelegates.Calculator;

public static class CalculatorProgram
{
    public static void Execute()
    {
        while (true)
        {
            Console.WriteLine("Введите первое число:");
        BankAccountLogger.LogInfo("Калькулятор. Просим ввести первое число");
        
        var number1 = Double.Parse(Console.ReadLine());
        BankAccountLogger.LogInfo("Калькулятор. Парсим первое число");

        Console.WriteLine("Выберите операцию + - * / ");
        BankAccountLogger.LogInfo("Калькулятор. Просим ввести операцию");
        
        var operation = Char.Parse(Console.ReadLine());
        BankAccountLogger.LogInfo("Калькулятор. Парсим операцию");

        Console.WriteLine("Введите второе число:");
        BankAccountLogger.LogInfo("Калькулятор. Просим ввести второе число");
        
        var number2 = Double.Parse(Console.ReadLine());
        BankAccountLogger.LogInfo("Калькулятор. Парсим второе число");

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

        }
        
    }
}