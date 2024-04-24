namespace BankAccountWithDelegates;

/// <summary>
/// класс банковсикй аккаунт 
/// </summary>
class BankAccount
{
    /// <summary>
    /// номер счета 
    /// </summary>
    public int AccountNumber { get; set; }
    
    /// <summary>
    /// имя фамилия владельца счета
    /// </summary>
    public string OwnerName { get; set; }
    
    /// <summary>
    /// баланс
    /// </summary>
    public double Balance { get; set; }
}