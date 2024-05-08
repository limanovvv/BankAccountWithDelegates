using BankAccountWithDelegates.BA;
using BankAccountWithDelegates.CreatedExceptions;
using Newtonsoft.Json;

namespace TestUnitCore;

[TestFixture]
public class TestBankAccount
{

    [SetUp]
    public void SetUp()
    {
        
    }
    
    [Test]
    public void PositiveCreateAccountTests()
    {
        
        BankAccountManager manager = new BankAccountManager();
        
        manager.CreateAccount("Alice", 1000);
        
        int countAccounts = BankAccountManager.accounts.Count;
        string ownerName = BankAccountManager.accounts[0].OwnerName;
        double balance = BankAccountManager.accounts[0].Balance;

        Assert.That(countAccounts, Is.EqualTo(1));
        Assert.That(ownerName, Is.EqualTo("Alice"));
        Assert.That(balance, Is.EqualTo(1000));

    }

    [Test]
    public void PositiveCreateAccountJsonTests()
    {

        string str = File.ReadAllText(BankAccountManager.JsonFile);
        BankAccount bankAccount = JsonConvert.DeserializeObject<List<BankAccount>>(str)[0];
        
        string ownerName = bankAccount.OwnerName;
        double balance = bankAccount.Balance;
        
        
        
    }
    
    [Test]
    public void NegativeCreateAccountTests()
    {
        
        BankAccountManager manager = new BankAccountManager();

        manager.CreateAccount("Alice", 1000);
        manager.CreateAccount("Mark", 1000);

        string actualResult1 = BankAccountManager.accounts[0].OwnerName;
        string actualResult2 = BankAccountManager.accounts[1].OwnerName;
        

        Assert.AreEqual("Alice", actualResult1);
        Assert.AreEqual("Mark", actualResult2);
    }

    [Test]
    public void PositiveWithdrawTests()
    {
        BankAccountManager manager = new BankAccountManager();

        manager.CreateAccount("Alice", 1000);
        manager.CreateAccount("Mark", 1000);

        int sum = 300;
        long accountNumber = BankAccountManager.accounts[0].AccountNumber;
        
        manager.Withdraw(accountNumber, sum);
        double balance = BankAccountManager.accounts[0].Balance;

        Assert.That(balance, Is.EqualTo(700));
    }
    
    [Test]
    public void NegativeWithdrawTests()
    {
        BankAccountManager manager = new BankAccountManager();

        manager.CreateAccount("Alice", 1000);
        manager.CreateAccount("Mark", 1000);

        long fakeAccountNumber = 111111111111;
        int sum = 15000;
        long accountNumber = BankAccountManager.accounts[0].AccountNumber;

        Assert.Throws<AccountNumberException>((() => manager.Withdraw(fakeAccountNumber, sum)));
        Assert.Throws<InsufficientFundsException>((() => manager.Withdraw(accountNumber, sum)));
        
    }
    
    
    
    
    
    
   
    
    
    
    
}