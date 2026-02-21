namespace Apps;

/// <summary>
/// Simple stateless arithmetic calculator.
/// Supports addition, subtraction, multiplication, and division.
/// </summary>
public static class Calculator
{
    /// <summary>
    /// Performs addition of two integers.
    /// </summary>
    /// <param name="x">The first integer.</param>
    /// <param name="y">The second integer.</param>
    /// <returns>The sum of the two integers.</returns>
    public static int Add(int x, int y) => x + y;

    /// <summary>
    /// Performs subtraction of two integers.
    /// </summary>
    /// <param name="x">The first integer.</param>
    /// <param name="y">The second integer.</param>
    /// <returns>The difference of the two integers.</returns>
    public static int Subtract(int x, int y) => x - y;

    /// <summary>
    /// Performs multiplication of two integers.
    /// </summary>
    /// <param name="x">The first integer.</param>
    /// <param name="y">The second integer.</param>
    /// <returns>The product of the two integers.</returns>
    public static int Multiply(int x, int y) => x * y;

    /// <summary>
    /// Performs division of two integers.
    /// </summary>
    /// <param name="x">The dividend.</param>
    /// <param name="y">The divisor.</param>
    /// <returns>The quotient of the two integers.</returns>
    /// <exception cref="DivideByZeroException">Thrown when the divisor is zero.</exception>
    public static int Divide(int x, int y)
    {
        if (y == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero.");
        }
        int calc = x / y;
        return calc;
    }
}
