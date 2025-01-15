using Xunit;
using MyBackendApp.Models;

public class CalculatorTests
{
    [Fact]
    public void AddTest()
    {
        var calc = new Calculator();
        var result = calc.Add(2, 3);
        Assert.Equal(5, result);
    }
    
    [Fact]
    public void SubtractTest()
    {
        var calc = new Calculator();
        var result = calc.Subtract(5, 3);
        Assert.Equal(2, result);
    }
}

