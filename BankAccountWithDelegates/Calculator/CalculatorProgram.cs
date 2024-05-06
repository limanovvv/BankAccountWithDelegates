namespace BankAccountWithDelegates.Calculator;

public static class CalculatorProgram
{
    public static void Execute()
    {
        Console.WriteLine("Введите первое число:");
        var number1 = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Выберите операцию + - * / ");
        var operation = Convert.ToChar(Console.ReadLine());

        Console.WriteLine("Введите второе число:");
        var number2 = Convert.ToDouble(Console.ReadLine());

        switch (operation)
        {
            case '+':
                CalculatorWithDelegates.Do(number1, number2, CalculatorWithDelegates.Add);
                break;
            case '-':
                CalculatorWithDelegates.Do(number1, number2, CalculatorWithDelegates.Substract);
                break;
            case '*':
                CalculatorWithDelegates.Do(number1, number2, CalculatorWithDelegates.Multiply);
                break;
            case '/':
                CalculatorWithDelegates.Do(number1, number2, CalculatorWithDelegates.Divide);
                break;
            default:
                Console.WriteLine("Введите корректную операцию + - * / ");
                break;
        }
    }
}