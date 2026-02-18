namespace Apps.Tests;

public class CalculatorTests
{
    [Fact]
    public void Calculate_ReturnsTwo()
    {
        var calculator = new Apps.Calculator();
        int result = calculator.Add(1, 1);
        Assert.Equal(2, result);
    }

    [Fact]
    public void Calculate_ReturnsCorrectSum()
    {
        var calculator = new Apps.Calculator();
        int expected = 1 + 1;
        int result = calculator.Add(1, 1);
        Assert.Equal(expected, result);
    }
}
