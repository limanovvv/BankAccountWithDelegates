namespace BankAccountWithDelegates.Calculator;

public delegate double Operation(double a, double b);

public static class CalculatorWithDelegates
{

    /// <summary>
    /// метод, который
    /// позволяет пользователю вводить числа
    /// и выполнять математические операции + - * /
    /// </summary>
    public static void Do(double a, double b, Operation operation)
    {
        Console.WriteLine($"Ответ: {Math.Round(operation(a, b), 4)}");
    }

    /// <summary>
    /// расчет суммы
    /// </summary>
    /// <param name="a"> первое слагаемое </param>
    /// <param name="b"> второе слагаемое </param>
    /// <returns> сумма </returns>
    public static double Add(double a, double b)
    {
        return a + b;
    }
    
    /// <summary>
    /// вычитание 
    /// </summary>
    /// <param name="a"> первый параметр </param>
    /// <param name="b"> второй параметр </param>
    /// <returns> разность </returns>
    public static double Substract(double a, double b)
    {
        return a - b;
    }
    
    /// <summary>
    /// умножение 
    /// </summary>
    /// <param name="a"> первый множитель </param>
    /// <param name="b"> второй множитель </param>
    /// <returns> умножение </returns>
    public static double Multiply(double a, double b)
    {
        return a * b;
    }
    
    /// <summary>
    /// деление 
    /// </summary>
    /// <param name="a"> первый параметр </param>
    /// <param name="b"> второй параметр (на что делим, то есть не может быть нуль) </param>
    /// <returns> результат деления </returns>
    public static double Divide(double a, double b)
    {
        double result;
        
        try
        {
            result = a / b;
        }
        catch (DivideByZeroException e)
        {
            throw;
        }

        return result;
    }
}