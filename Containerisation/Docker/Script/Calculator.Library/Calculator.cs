using System.Numerics;

namespace Calculator.Library;

/// <summary>
/// A simple calculator library that provides basic arithmetic operations.
/// </summary>
public static class Calculator
{
    /// <summary>
    /// Performs addition of two integers.
    /// </summary>
    /// <param name="x">The first integer.</param>
    /// <param name="y">The second integer.</param>
    /// <returns>The sum of the two integers.</returns>
    public static T Add<T>(T x, T y) where T : INumber<T> => checked(x + y);

    /// <summary>
    /// Performs subtraction of two integers.
    /// </summary>
    /// <param name="x">The minuend.</param>
    /// <param name="y">The subtrahend.</param>
    /// <returns>The difference of the two integers.</returns>
    public static T Subtract<T>(T x, T y) where T : INumber<T> => checked(x - y);

    /// <summary>
    /// Performs multiplication of two integers.
    /// </summary>
    /// <param name="x">The first integer.</param>
    /// <param name="y">The second integer.</param>
    /// <returns>The product of the two integers.</returns>
    public static T Multiply<T>(T x, T y) where T : INumber<T> => checked(x * y);

    /// <summary>
    /// Performs division of two integers.
    /// </summary>
    /// <param name="x">The dividend.</param>
    /// <param name="y">The divisor.</param>
    /// <returns>The quotient of the two integers.</returns>
    public static T Divide<T>(T x, T y) where T : INumber<T>
    {
        if (y == T.Zero) { throw new DivideByZeroException("Cannot divide by zero."); }
        return x / y;
    }

    /// <summary>
    /// Performs division of two integers, returning a boolean to indicate success or failure.
    /// </summary>
    /// <typeparam name="T">The numeric type.</typeparam>
    /// <param name="x">The dividend.</param>
    /// <param name="y">The divisor.</param>
    /// <param name="result">The result of the division if successful; otherwise, zero.</param>
    /// <returns>True if the division was successful; otherwise, false.</returns>
    public static bool TryDivide<T>(T x, T y, out T result) where T : INumber<T>
    {
        if (y == T.Zero)
        {
            result = T.Zero;
            return false;
        }

        result = x / y;
        return true;
    }
}