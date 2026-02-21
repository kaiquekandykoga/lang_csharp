using Apps;

var tests = new (int x, int y)[]
{
    (1, 1),
    (1, 0)
};

var operations = new List<(string Name, Func<int, int, int> Execute)>
{
    ("Add", Calculator.Add),
    ("Subtract", Calculator.Subtract),
    ("Multiply", Calculator.Multiply),
    ("Divide", Calculator.Divide),
    ("TryDivide", (x, y) =>
    {
        return Calculator.TryDivide(x, y, out int result)
            ? result
            : throw new DivideByZeroException("Cannot divide by zero.");
    })
};

foreach (var op in operations)
{
    foreach (var (x, y) in tests)
    {
        try
        {
            Console.WriteLine($"Calculator.{op.Name}({x}, {y}) → {op.Execute(x, y)}");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"Calculator.{op.Name}({x}, {y}) → Exception: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Calculator.{op.Name}({x}, {y}) → Unexpected Exception: {ex.Message}");
        }
    }
}
