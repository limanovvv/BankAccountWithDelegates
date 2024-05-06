using BankAccountWithDelegates.CreatedExceptions;
using Newtonsoft.Json;

namespace BankAccountWithDelegates.BA;

/// <summary>
/// делегат БанковскаяОперация
/// </summary>
public delegate void BankOperation(long accountNumber, double amount);

/// <summary>
/// класс для управления банковскими аккаунтами и операциями
/// </summary>
public class BankAccountManager
{
    
    /// <summary>
    /// приватный лист для хранения аккаунтов
    /// </summary>
    public List<BankAccount> accounts = new List<BankAccount>();

    /// <summary>
    /// имя json файла для хранения аккаунтов
    /// </summary>
    private const string JsonFile = "jsonForBankAccounts";
    
    /// <summary>
    /// длина банковского счета
    /// по заданию равная 12
    /// </summary>
    private const int AccountNumberLength = 12;

    /// <summary>
    /// создание аккаунта через менеджер
    /// </summary>
    /// <param name="ownerName"> владелец аккаунта </param>
    /// <param name="initialBalance"> начальный баланс </param>
    public void CreateAccount(string ownerName, double initialBalance)
    {
        try
        {
            BankAccount newAccount = new BankAccount
            {
                AccountNumber = GetGeteratedAccountNumber(AccountNumberLength),
                OwnerName = ownerName,
                Balance = initialBalance,
                
            };

            string json = JsonConvert.SerializeObject(newAccount);
            File.WriteAllText(JsonFile, json);

            accounts.Add(newAccount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    } 
    
    /// <summary>
    /// операция депозит - зачисление средств на счет
    /// используя linq и делегаты
    /// </summary>
    /// <param name="accountNumber"> номер счета </param>
    /// <param name="amount"> сумма </param>
    public void Deposit(long accountNumber, double amount)
    {
        try
        {
            
            BankOperation deposit = (accNum, amt) =>
            {
                var account = accounts.Find(acc => acc.AccountNumber == accNum);
                if (account != null) account.Balance += amt;
                
                BankAccountLogger.LogInfo($"Зачисление средств на счет\n номер аккаунта: {accountNumber}\n сумма зачисления {amount}\n баланс {account.Balance}");
            };

            deposit(accountNumber, amount);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    /// <summary>
    /// операция снятия средств со счета
    /// используя linq и делегаты
    /// </summary>
    /// <param name="accountNumber"> номер счета </param>
    /// <param name="amount"> сумма </param>
    /// <exception cref="AccountNumberException"> не нашелся счет в списке </exception>
    /// <exception cref="InsufficientFundsException"> недостаточно средств</exception>
    public void Withdraw(long accountNumber, double amount)
    {
        BankOperation withdraw = (accNum, amt) =>
        {
            var account = accounts.Find(acc => acc.AccountNumber == accNum);

            switch (account)
            {
                case null:
                    BankAccountLogger.LogError($"Ошибка! Снятие средств со счета\n Ошибка номера аккаунта. Номера {amount} не существует\n");
                    throw new AccountNumberException($"Ошибка номера аккаунта. Номера {amount} не существует");
                
                default:
                {
                    if (account.Balance < amount)
                    {
                        BankAccountLogger.LogError($"Ошибка! Снятие средств со счета\n Недостаточно средств на счете\n номер: {amount}\n баланс: {account.Balance}");
                        
                        throw new InsufficientFundsException("Недостаточно средств на счете", account.Balance);
                    }
                    else
                    {
                        account.Balance -= amount;
                        BankAccountLogger.LogInfo($"Снятие средств со счета\n номер аккаунта: {accountNumber}\n сумма снятия {amount}\n баланс {account.Balance}");
                    }

                    break;
                }
            }
        };

        withdraw(accountNumber, amount);
    }

    
    /// <summary>
    /// операция перевод со счета на счет
    /// </summary>
    /// <param name="fromAccountNumber"> кто переводит </param>
    /// <param name="toAccountNumber"> кому переводят </param>
    /// <param name="amount"> сумма </param>
    public void Transfer(long fromAccountNumber, long toAccountNumber, double amount)
    {
        
            BankAccountLogger.LogInfo($"Перевод\n номер аккаунта отправителя: {fromAccountNumber}\n номер аккаунта получателя {toAccountNumber}\n сумма перевода {amount}");
            Withdraw(fromAccountNumber, amount);
            Deposit(toAccountNumber, amount); 
            
    }
    
    /// <summary>
    /// вывод баланса
    /// </summary>
    /// <param name="accountNumber"> номер счета </param>
    public void PrintBalance(long accountNumber)
    {
        var account = accounts.Find(acc => acc.AccountNumber == accountNumber);
        if (account != null)
        {
            Console.WriteLine($"Account {accountNumber}: Balance = {account.Balance}");
        }
        else
        {
            Console.WriteLine($"Account {accountNumber} not found");
        }
    }

    /// <summary>
    /// метод для генерации банковского счета
    /// </summary>
    /// <param name="length"> сколько цифр нужно сгенерировать </param>
    /// <returns> рандомная последовательность цифр </returns>
    private long GetGeteratedAccountNumber(int length)
    {
        Random random = new Random();
        string str = String.Empty;

        for (int i = 0; i < length; i++)
        {
            str += random.Next(10);
        }

        return long.Parse(str);
    }



}