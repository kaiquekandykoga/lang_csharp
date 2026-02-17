namespace Apps.Tests;

public class CalculatorTests
{
    [Fact]
    public void Calculate_ReturnsTwo()
    {
        var calculator = new Apps.Calculator();
        int result = calculator.Calculate();
        Assert.Equal(2, result);
    }

    [Fact]
    public void Calculate_ReturnsCorrectSum()
    {
        var calculator = new Apps.Calculator();
        int expected = 1 + 1;
        int result = calculator.Calculate();
        Assert.Equal(expected, result);
    }
}
