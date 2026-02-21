namespace Apps;

public class Calculator
{
    public int Add(int x, int y)
    {
        int calc = x + y;
        return calc;
    }

    public int Subtract(int x, int y)
    {
        int calc = x - y;
        return calc;
    }

    public int Multiply(int x, int y)
    {
        int calc = x * y;
        return calc;
    }

    public int Divide(int x, int y)
    {
        if (y == 0)
        {
            throw new DivideByZeroException("Cannot divide by zero.");
        }
        int calc = x / y;
        return calc;
    }
}
