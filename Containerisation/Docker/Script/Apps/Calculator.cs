namespace Apps;

public static class Calculator
{
    public static int Add(int x, int y) => x + y;

    public static int Subtract(int x, int y) => x - y;

    public static int Multiply(int x, int y) => x * y;

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
