using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    /// имя json файла для хранения аккаунтов
    /// </summary>
    public static string JsonFile = Path.GetFullPath(AppDomain.CurrentDomain + "/../../../../../jsonForBankAccounts.json");

    static BankAccountManager()
    {
        List<BankAccount>? list;

        if (File.Exists(JsonFile))
        {
            try
            {
                string str = File.ReadAllText(JsonFile);
                accounts = JsonConvert.DeserializeObject<List<BankAccount>>(str);
            }
            catch (JsonException)
            {
                accounts = new List<BankAccount>();
            }
        }
        else
        {
            accounts = new List<BankAccount>();
        }
    }


    /// <summary>
    /// лист для хранения аккаунтов
    /// </summary>
    public static List<BankAccount>? accounts { get; }

    /// <summary>
    /// длина банковского счета
    /// по заданию равная 12
    /// </summary>
    private const int AccountNumberLength = 12;

    public BankAccount GetAccount(long accountNumber)
    {
        try
        {
            return accounts.Single(e => e.AccountNumber == accountNumber);
        }
        catch (AccountNumberException)
        {
            throw;
        }
    }


    /// <summary>
    /// создание аккаунта через менеджер
    /// </summary>
    /// <param name="ownerName"> владелец аккаунта </param>
    /// <param name="initialBalance"> начальный баланс </param>
    public long CreateAccount(string ownerName, double initialBalance)
    {
        try
        {
            if (ownerName == null || ownerName.ToCharArray().Count() < 2)
            {
                throw new ArgumentException("Некорректное имя пользователя");
            }

            if (initialBalance < 0)
            {
                throw new ArgumentException("Некорректная сумма баланса");
            }

            BankAccount newAccount = new BankAccount
            {
                AccountNumber = GetGeneratedAccountNumber(AccountNumberLength),
                OwnerName = ownerName,
                Balance = initialBalance
                
            };

            Logger.LogInfo("в методе CreateAccount первым делом создался новый объект BankAccount");

            accounts.Add(newAccount);
            Logger.LogInfo("добавляем в лист новый объект");
            
            string json = JsonConvert.SerializeObject(accounts);
            Logger.LogInfo("сериализуем лист с этим новым объектом в json-сроку");

            File.WriteAllText(JsonFile, json);
            Logger.LogInfo("записываем в файл JsonFile json-строку");

            return newAccount.AccountNumber;
        }
        catch (JsonException e)
        {
            Logger.LogError($"Произошла ошибка при сериализации в JSON в методе CreateAccount: {e.Message}");
            throw;
        }
        catch (IOException e)
        {
            Logger.LogError($"Произошла ошибка ввода-вывода в методе CreateAccount: {e.Message}");
            throw;
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
                if (account != null)
                {
                    account.Balance += amt;

                    string json = JsonConvert.SerializeObject(accounts);
                    File.WriteAllText(JsonFile, json);
                    
                    Logger.LogInfo($"Зачисление средств на счет\n номер аккаунта: {accountNumber}\n сумма зачисления {amount}\n баланс {account.Balance}");
                }
                
            };

            deposit(accountNumber, amount);
            
        }
        catch (JsonException e)
        {
            Logger.LogError($"Произошла ошибка при сериализации в JSON в методе Deposit: {e.Message}");
            throw;
        }
        catch (IOException e)
        {
            Logger.LogError($"Произошла ошибка ввода-вывода в методе Deposit: {e.Message}");
            throw;
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
        if (amount < 0)
        {
            throw new ArgumentException("Отрицательная сумма снятия");
        }
        if (accountNumber.ToString().Length != AccountNumberLength)
        {
            throw new AccountNumberException("Ошибка номера аккаунта. Некорректное количество символов");
        }
        
        BankOperation withdraw = (accNum, amt) =>
        {
            var account = accounts.Find(acc => acc.AccountNumber == accNum);

            switch (account)
            {
                case null:
                    Logger.LogError($"Ошибка! Снятие средств со счета\n Ошибка номера аккаунта. Номера {amount} не существует\n");
                    throw new AccountNumberException($"Ошибка номера аккаунта. Номера {amount} не существует");
                
                default:
                {
                    if (account.Balance < amount)
                    {
                        Logger.LogError($"Ошибка! Снятие средств со счета\n Недостаточно средств на счете\n номер: {amount}\n баланс: {account.Balance}");
                        
                        throw new InsufficientFundsException("Недостаточно средств на счете", account.Balance);
                    }
                    else
                    {
                        account.Balance -= amount;
                        
                        string json = JsonConvert.SerializeObject(accounts);
                        File.WriteAllText(JsonFile, json);
                        
                        Logger.LogInfo($"Снятие средств со счета\n номер аккаунта: {accountNumber}\n сумма снятия {amount}\n баланс {account.Balance}");
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
        
            Logger.LogInfo($"Перевод\n номер аккаунта отправителя: {fromAccountNumber}\n номер аккаунта получателя {toAccountNumber}\n сумма перевода {amount}");
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
    private long GetGeneratedAccountNumber(int length)
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