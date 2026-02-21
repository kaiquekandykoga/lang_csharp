using Apps;

var tests = new(int x, int y)[]
{
    (1, 1),
    (1, 0)
};

foreach (var (x, y) in tests)
{
    Console.WriteLine($"Calculator.Add({x}, {y}) → {Calculator.Add(x, y)}");
}

foreach (var (x, y) in tests)
{
    Console.WriteLine($"Calculator.Subtract({x}, {y}) → {Calculator.Subtract(x, y)}");
}

foreach (var (x, y) in tests)
{
    Console.WriteLine($"Calculator.Multiply({x}, {y}) → {Calculator.Multiply(x, y)}");
}

foreach (var (x, y) in tests)
{
    try
    {
        Console.WriteLine($"Calculator.Divide({x}, {y}) → {Calculator.Divide(x, y)}");
    }
    catch (DivideByZeroException ex)
    {
        Console.WriteLine($"Calculator.Divide({x}, {y}) → Exception: {ex.Message}");
    }
}

foreach (var (x, y) in tests)
{
    if (Calculator.TryDivide(x, y, out int result))
    {
        Console.WriteLine($"Calculator.TryDivide({x}, {y}) → Success: {result}");
    }
    else
    {
        Console.WriteLine($"Calculator.TryDivide({x}, {y}) → Failed: Division by zero.");
    }
}
