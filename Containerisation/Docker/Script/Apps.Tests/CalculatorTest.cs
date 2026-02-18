namespace Apps.Tests;

public class CalculatorTests
{
    [Fact]
    public void Add_ReturnsTwo()
    {
        var calculator = new Apps.Calculator();
        int result = calculator.Add(1, 1);
        Assert.Equal(2, result);
    }
}
