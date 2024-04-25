namespace Calculator;

public delegate double Operation(double a, double b);

public class CalculatorWithDelegates
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

    public static double Add(double a, double b)
    {
        return a + b;
    }
    
    public static double Substract(double a, double b)
    {
        return a - b;
    }
    
    public static double Multiply(double a, double b)
    {
        return a * b;
    }
    
    public static double Divide(double a, double b)
    {
        return a / b;
    }
}