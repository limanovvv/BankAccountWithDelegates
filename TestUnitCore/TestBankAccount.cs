using BankAccountWithDelegates.BA;
using BankAccountWithDelegates.CreatedExceptions;
using Newtonsoft.Json;

namespace TestUnitCore;

[TestFixture]
public class TestBankAccount
{
    private BankAccountManager manager;
    
    
    
    [Test]
    public void PositiveCreateAccountTests()
    {
        manager.CreateAccount("Alice", 1000);
        
        int countAccounts = BankAccountManager.accounts.Count;
        string ownerName = BankAccountManager.accounts[0].OwnerName;
        double balance = BankAccountManager.accounts[0].Balance;

        Assert.That(countAccounts, Is.EqualTo(1), "Неправильное количество аккаунтов");
        Assert.That(ownerName, Is.EqualTo("Alice"), "Неправильное имя владельца");
        Assert.That(balance, Is.EqualTo(1000), "Неправильный баланс");

    }

    [Test]
    public void PositiveCreateAccountJsonTests()
    {

        string str = File.ReadAllText(BankAccountManager.JsonFile);
        BankAccount bankAccount = JsonConvert.DeserializeObject<List<BankAccount>>(str)[0];
        
        string ownerName = bankAccount.OwnerName;
        double balance = bankAccount.Balance;
        
        Assert.AreEqual("Alice", ownerName, "Неправильное имя владельца в json файле");
        Assert.AreEqual(1000, balance, "Неправильный баланс в json файле");
        
    }

    [Test]
    public void PositiveWithdrawTest()
    {

        int sum = 300;
        long accountNumber = BankAccountManager.accounts[0].AccountNumber;
        
        manager.Withdraw(accountNumber, sum);
        double balance = BankAccountManager.accounts[0].Balance;

        Assert.That(balance, Is.EqualTo(700), "Неправильный баланс после операции перевода");
    }
    
    [Test]
    public void NegativeWithdraw_AccountNumberException_Test()
    {
        long fakeAccountNumber = 111111111111;
        int sum = 150;

        Assert.Throws<AccountNumberException>((() => manager.Withdraw(fakeAccountNumber, sum)), "Ожидалось исключение AccountNumberException при попытке снятия средств с несуществующего аккаунта");
    }

    [Test]
    public void NegativeWithdraw_InsufficientFundsException_Test()
    {
        int sum = 15000;
        long accountNumber = BankAccountManager.accounts[0].AccountNumber;
        
        Assert.Throws<InsufficientFundsException>((() => manager.Withdraw(accountNumber, sum)), "Ожидалось исключение InsufficientFundsException при попытке снятия средств с несуществующего аккаунта");
    }
    
    
    
    
    
    
   
    
    
    
    
}