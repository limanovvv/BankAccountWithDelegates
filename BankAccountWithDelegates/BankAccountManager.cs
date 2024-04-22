using System.Linq.Expressions;
using System.Xml;

namespace BankAccountWithDelegates;

public delegate void BankOperation(int accountNumber, double amount);

public class BankAccountManager
{
    private List<BankAccount> accounts = new List<BankAccount>();

    public void CreateAccount(string ownerName, double initialBalance)
    {
        try
        {
            BankAccount newAccount = new BankAccount
            {
                AccountNumber = accounts.Count + 1,
                OwnerName = ownerName,
                Balance = initialBalance
            };

            accounts.Add(newAccount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    } 
    public void Deposit(int accountNumber, double amount)
    {
        try
        {
            BankOperation deposit = (accNum, amt) =>
            {
                var account = accounts.Find(acc => acc.AccountNumber == accNum);
                if (account != null) account.Balance += amt;
            };

            deposit(accountNumber, amount);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
    
    public void Withdraw(int accountNumber, double amount)
    {
        BankOperation withdraw = (accNum, amt) =>
        {
            var account = accounts.Find(acc => acc.AccountNumber == accNum);

            switch (account)
            {
                case null:
                    throw new AccountNumberException($"Ошибка номера аккаунта. Номера {amount} не существует");
                default:
                {
                    if (account.Balance < amount)
                    {
                        throw new InsufficientFundsException("", account.Balance);
                    }

                    break;
                }
            }
        };

        withdraw(accountNumber, amount);
    }

    public void Transfer(int fromAccountNumber, int toAccountNumber, double amount)
    {
            Withdraw(fromAccountNumber, amount);
            Deposit(toAccountNumber, amount);

    }
    
    public void PrintBalance(int accountNumber)
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




}