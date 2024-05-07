using BankAccountWithDelegates.BA;

namespace TestUnitCore;

[TestFixture]
public class TestBankAccount
{

    [SetUp]
    public void SetUp()
    {
        
    }
    
    [Test]
    public void CreateAccount_NewAccount_Test()
    {
        manager.CreateAccount("Alice", 1000);
        manager.CreateAccount("Mark", 1000);
        
        BankAccountManager manager = new BankAccountManager();

        manager.CreateAccount("Alice", 1000);

        double actualResult1 = BankAccountManager.accounts[0].Balance;

        Assert.That(BankAccountManager.accounts.Count, Is.EqualTo(1));
    }
    
    [Test]
    public void CreateAccount_OwnerName_Test()
    {
        
        BankAccountManager manager = new BankAccountManager();

        manager.CreateAccount("Alice", 1000);
        manager.CreateAccount("Mark", 1000);

        string actualResult1 = BankAccountManager.accounts[0].OwnerName;
        string actualResult2 = BankAccountManager.accounts[1].OwnerName;
        

        Assert.AreEqual("Alice", actualResult1);
        Assert.AreEqual("Mark", actualResult2);
    }
    
    
    
   
    
    
    
    
}