using Apps;

var calculator = new Calculator();

var tests = new(int x, int y)[]
{
    (1, 1),
    (1, 0)
};

foreach (var (x, y) in tests)
{
    Console.WriteLine($"calculator.Add({x}, {y}) → {calculator.Add(x, y)}");
}

foreach (var (x, y) in tests)
{
    Console.WriteLine($"calculator.Subtract({x}, {y}) → {calculator.Subtract(x, y)}");
}

foreach (var (x, y) in tests)
{
    Console.WriteLine($"calculator.Multiply({x}, {y}) → {calculator.Multiply(x, y)}");
}

foreach (var (x, y) in tests)
{
    try
    {
        Console.WriteLine($"calculator.Divide({x}, {y}) → {calculator.Divide(x, y)}");
    }
    catch (DivideByZeroException ex)
    {
        Console.WriteLine($"calculator.Divide({x}, {y}) → Exception: {ex.Message}");
    }
}
