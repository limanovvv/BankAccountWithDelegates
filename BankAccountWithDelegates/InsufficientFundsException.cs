namespace BankAccountWithDelegates;

/// <summary>
/// создаем свой тип ошибки Недостаточно средств
/// наследуемся от базового Exception
/// </summary>
public class InsufficientFundsException : Exception
{
    /// <summary>
    /// баланс
    /// </summary>
    public double Sum;

    /// <summary>
    /// конструктор с двумя параметрами
    /// </summary>
    /// <param name="message"> сообщение об ошибке </param>
    /// <param name="sum"> баланс </param>
    public InsufficientFundsException(string message, double sum) : base(message)
    {
        Sum = sum;
    }
}