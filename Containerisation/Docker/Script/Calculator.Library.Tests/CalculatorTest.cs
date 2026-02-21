using Calculator.Library;

namespace Calculator.Library.Tests;

public class CalculatorTests
{
    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(-5, 2, -3)]
    [InlineData(0, 0, 0)]
    public void Add_ReturnsExpectedResult(int x, int y, int expected)
    {
        int result = Calculator.Add(x, y);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(3, 1, 2)]
    [InlineData(1, 3, -2)]
    [InlineData(0, 0, 0)]
    public void Subtract_ReturnsExpectedResult(int x, int y, int expected)
    {
        int result = Calculator.Subtract(x, y);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(3, 4, 12)]
    [InlineData(-3, 4, -12)]
    [InlineData(10, 0, 0)]
    public void Multiply_ReturnsExpectedResult(int x, int y, int expected)
    {
        int result = Calculator.Multiply(x, y);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(8, 2, 4)]
    [InlineData(9, 2, 4)]
    [InlineData(-9, 2, -4)]
    public void Divide_ReturnsExpectedResult(int x, int y, int expected)
    {
        int result = Calculator.Divide(x, y);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Divide_WhenDivisorIsZero_ThrowsDivideByZeroException()
    {
        Assert.Throws<DivideByZeroException>(() => Calculator.Divide(1, 0));
    }

    [Fact]
    public void TryDivide_WhenDivisorIsNonZero_ReturnsTrueAndResult()
    {
        bool success = Calculator.TryDivide(8, 2, out int result);

        Assert.True(success);
        Assert.Equal(4, result);
    }

    [Fact]
    public void TryDivide_WhenDivisorIsZero_ReturnsFalseAndDefaultResult()
    {
        bool success = Calculator.TryDivide(8, 0, out int result);

        Assert.False(success);
        Assert.Equal(0, result);
    }

    [Fact]
    public void Add_WithDecimal_ReturnsExpectedResult()
    {
        decimal result = Calculator.Add(1.5m, 2.25m);

        Assert.Equal(3.75m, result);
    }

    [Fact]
    public void Divide_WithDouble_ReturnsExpectedResult()
    {
        double result = Calculator.Divide(7.5d, 2.5d);

        Assert.Equal(3d, result);
    }

    [Fact]
    public void TryDivide_WithDecimalAndZeroDivisor_ReturnsFalseAndZero()
    {
        bool success = Calculator.TryDivide(10m, 0m, out decimal result);

        Assert.False(success);
        Assert.Equal(0m, result);
    }
}
