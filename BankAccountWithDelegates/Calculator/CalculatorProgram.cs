namespace BankAccountWithDelegates.Calculator;

public static class CalculatorProgram
{
    public static void Execute()
    {
        while (true)
        {
            Console.WriteLine("Введите первое число:");
        Logger.LogInfo("Калькулятор. Просим ввести первое число");
        
        var number1 = Double.Parse(Console.ReadLine());
        Logger.LogInfo("Калькулятор. Парсим первое число");

        Console.WriteLine("Выберите операцию + - * / ");
        Logger.LogInfo("Калькулятор. Просим ввести операцию");
        
        var operation = Char.Parse(Console.ReadLine());
        Logger.LogInfo("Калькулятор. Парсим операцию");

        Console.WriteLine("Введите второе число:");
        Logger.LogInfo("Калькулятор. Просим ввести второе число");
        
        var number2 = Double.Parse(Console.ReadLine());
        Logger.LogInfo("Калькулятор. Парсим второе число");

        switch (operation)
        {
            case '+':
                
                CalculatorWithDelegates.Do(number1, number2, CalculatorWithDelegates.Add);
                Logger.LogInfo("Калькулятор. Складываем");
                
                break;
            case '-':
                
                CalculatorWithDelegates.Do(number1, number2, CalculatorWithDelegates.Substract);
                Logger.LogInfo("Калькулятор. Вычитаем");
                
                break;
            case '*':
                
                CalculatorWithDelegates.Do(number1, number2, CalculatorWithDelegates.Multiply);
                Logger.LogInfo("Калькулятор. Умножаем");
                
                break;
            case '/':

                try
                {
                    
                    CalculatorWithDelegates.Do(number1, number2, CalculatorWithDelegates.Divide);
                    Logger.LogInfo("Калькулятор. Делим");
                    
                }
                catch (DivideByZeroException e)
                {
                    
                    Logger.LogError("Калькулятор. Ошибка. Нельзя делить на нуль");
                    Console.WriteLine(e);
                    
                }
                
                break;
            default:
                
                Console.WriteLine("Операция была введена неккоректно + - * / ");
                Logger.LogInfo("Калькулятор. Операция была введена неккоректно");
                
                break;
        }

        }
        
    }
}