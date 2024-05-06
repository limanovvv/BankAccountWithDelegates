namespace BankAccountWithDelegates.BA;

/// <summary>
/// класс банковсикй аккаунт 
/// </summary>
public class BankAccount
{
    /// <summary>
    /// номер счета 
    /// </summary>
    public long AccountNumber { get; set; }
    
    /// <summary>
    /// имя фамилия владельца счета
    /// </summary>
    public string OwnerName { get; set; }
    
    /// <summary>
    /// баланс
    /// </summary>
    public double Balance { get; set; }
}