namespace BankAccountWithDelegates.CreatedExceptions;

/// <summary>
/// создаем свой тип ошибки счета
/// наследуемся от базового Exception
/// </summary>
public class AccountNumberException : Exception
{ 
    /// <summary>
    /// конструктор с одним параметром
    /// который передаем в базовый конструктор Exception
    /// </summary>
    /// <param name="message"> сообщение </param>
    public AccountNumberException(string message) : base(message) {}
}