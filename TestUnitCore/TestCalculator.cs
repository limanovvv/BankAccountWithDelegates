using BankAccountWithDelegates.Calculator;

namespace TestUnitCore;

[TestFixture]
public class TestCalculator
{
    
    [TestCase(2, 3, '+', 5)]
    [TestCase(5, 3, '-', 2)]
    [TestCase(2, 3, '*', 6)]
    [TestCase(6, 3, '/', 2)]
    public void CalculatorTest(double a, double b, char operation, double expectedResult)
    {
        
        double actualResult;
        
        switch (operation)
        {
            case '+':
                actualResult = CalculatorWithDelegates.Add(a, b);
                break;
            case '-':
                actualResult = CalculatorWithDelegates.Substract(a, b);
                break;
            case '*':
                actualResult = CalculatorWithDelegates.Multiply(a, b);
                break;
            case '/':
                actualResult = CalculatorWithDelegates.Divide(a, b);
                break;
            default:
                throw new ArgumentException("Неподдерживаемая операция", nameof(operation));
        }
        
        Assert.AreEqual(expectedResult, actualResult);
            
    }
}