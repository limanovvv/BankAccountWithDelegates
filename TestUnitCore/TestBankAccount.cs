using BankAccountWithDelegates.BA;
using BankAccountWithDelegates.CreatedExceptions;
using Newtonsoft.Json;

namespace TestUnitCore;

[TestFixture]
public class TestBankAccount
{
    private BankAccountManager manager = new BankAccountManager();

    // [Test(Description = "Проверка создания аккаунта. Увеличение количества аккаунтов на 1. Правильное имя и баланс.")]
    // public void PositiveCreateAccountTests()
    // {
    //     int previousAccountsCount = BankAccountManager.accounts.Count;
    //     
    //     manager.CreateAccount("Alice", 1000);
    //     
    //     int accountsCount = BankAccountManager.accounts.Count;
    //
    //     Assert.That(accountsCount, Is.EqualTo(previousAccountsCount + 1), "Неправильное количество аккаунтов");
    //     
    //     Assert.Multiple(() =>
    //         {
    //             string ownerName = BankAccountManager.accounts[accountsCount - 1].OwnerName;
    //             double balance = BankAccountManager.accounts[accountsCount - 1].Balance;
    //     
    //             Assert.That(ownerName, Is.EqualTo("Alice"), "Неправильное имя владельца");
    //             Assert.That(balance, Is.EqualTo(1000), "Неправильный баланс");
    //         });
    //     
    // }

    [TestCase("Li", 500, TestName = "Проверка создания аккаунта. Имя с двумя символами")]
    [TestCase("Lim", 500, TestName = "Проверка создания аккаунта. Имя с тремя символами")]
    [TestCase("Limanov Valeriy", 500, TestName = "Проверка создания аккаунта. Имя с 15 символами")]
    public void PositiveCreateAccount_OwnerName_Test(string ownerName, double initialBalance)
    {
        int accountsCount = BankAccountManager.accounts.Count;
        
        long accountNumber = manager.CreateAccount(ownerName, initialBalance);

        string actualOwnerName = BankAccountManager.accounts[accountsCount - 1].OwnerName;

        Assert.That(actualOwnerName, Is.EqualTo(ownerName), "Неправильное имя владельца");
    }
    
    [TestCase("", 500, TestName = "Негативная проверка создания аккаунта. Имя без символов")]
    [TestCase("p", 500, TestName = "Негативная проверка создания аккаунта. Имя с одним символом")]
    [TestCase(null, 500, TestName = "Негативная проверка создания аккаунта. Имя null")]
    public void NegativeCreateAccount_OwnerName_Test(string fakeOwnerName, double initialBalance)
    {
        
        Assert.Throws<ArgumentException>((() => manager.CreateAccount(fakeOwnerName, initialBalance)), "Ожидалось исключение ArgumentException при попытке осздания аккаунта с некорректным именем");
        
    }

    [TestCase("Vasiliy Paketov", 0, TestName = "Проверка создания аккаунта. Нулевой баланс")]
    [TestCase("Vasiliy Paketov", 10000, TestName = "Проверка создания аккаунта. Баланс 10000")]
    public void PositiveCreateAccount_Balance_Test(string ownerName, double initialBalance)
    {
        int accountsCount = BankAccountManager.accounts.Count;
        
        long accountNumber = manager.CreateAccount(ownerName, initialBalance);

        double actualBalance = BankAccountManager.accounts[accountsCount - 1].Balance;

        Assert.That(actualBalance, Is.EqualTo(initialBalance), "Неправильный баланс");
        
    }
    
    [Test]
    public void NegativeCreateAccount_Balance_Test()
    {
        string ownerName = "Petr";
        double initialBalance = -100;

        Assert.Throws<ArgumentException>((() => manager.CreateAccount(ownerName, initialBalance)), "Ожидалось исключение ArgumentException при попытке осздания аккаунта с отрицательным балансом");
    }

    [TestCase(26424944652, 1200,TestName = "Позитивная проверка операции снятия. Снимаем все что есть на балансе 1200")]
    [TestCase(26424944652, 1199.99,TestName = "Позитивная проверка операции снятия. Снимаем 1199.99")]
    [TestCase(26424944652, 100,TestName = "Позитивная проверка операции снятия. Снимаем 100")]
    public void PositiveWithdrawTest(long accountNumber,double sum)
    {
        BankAccount bankAccount = manager.GetAccount(accountNumber);
        double previousBalance = bankAccount.Balance;
        
        manager.Withdraw(accountNumber, sum);
        
        double actualBalance = bankAccount.Balance;

        Assert.That(actualBalance, Is.EqualTo(previousBalance - sum), "Неправильный баланс после операции перевода");
        
        manager.Deposit(accountNumber, sum);
    }
    
    [TestCase(111111111111, 150, TestName = "Негативная проверка операции снятия. Неккоректный номер аккаунта из 12 символов")]
    [TestCase(2642494465, 150, TestName = "Негативная проверка операции снятия. Номер аккаунта из 11 символов")]
    [TestCase(264249446520, 150, TestName = "Негативная проверка операции снятия. Номер аккаунта из 13 символов")]
    public void NegativeWithdraw_AccountNumberException_Test(long accountNumber, double sum)
    {
        
        Assert.Throws<AccountNumberException>((() => manager.Withdraw(accountNumber, sum)), "Ожидалось исключение AccountNumberException при попытке снятия средств с несуществующего аккаунта");
        
    }


    [TestCase(26424944652, 1200.01, TestName = "Негативная проверка операции снятия. Сумма снятия больше баланса на 0.01")]
    [TestCase(26424944652, 15000, TestName = "Негативная проверка операции снятия. Сумма снятия больше баланса на 13800")]
    public void NegativeWithdraw_InsufficientFundsException_Test(long accountNumber, double sum)
    {
        
        Assert.Throws<InsufficientFundsException>((() => manager.Withdraw(accountNumber, sum)), "Ожидалось исключение InsufficientFundsException при попытке снятия средств с несуществующего аккаунта");
        
    }

    [TestCase(26424944652, -0.01, TestName = "Негативная проверка операции снятия. Отрицательная сумма снятия -0.01")]
    [TestCase(26424944652, -1200, TestName = "Негативная проверка операции снятия. Отрицательная сумма снятия -1200")]
    public void NegativeWithdraw_ArgumentException_Test(long accountNumber, double sum)
    {
        
        Assert.Throws<ArgumentException>((() => manager.Withdraw(accountNumber, sum)), "Ожидалось исключение ArgumentException при попытке снятия средств с несуществующего аккаунта");
        
    }
}