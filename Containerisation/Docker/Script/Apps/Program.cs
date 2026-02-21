using Apps;

var calculator = new Calculator();

Console.WriteLine($"calculator.Add(1, 1) → {calculator.Add(1, 1)}");
Console.WriteLine($"calculator.Add(1, 0) → {calculator.Add(1, 0)}");

Console.WriteLine($"calculator.Subtract(1, 1) → {calculator.Subtract(1, 1)}");

Console.WriteLine($"calculator.Multiply(1, 1) → {calculator.Multiply(1, 1)}");

try
{
    Console.WriteLine($"calculator.Divide(1, 0) → {calculator.Divide(1, 0)}");
}
catch (DivideByZeroException ex)
{
    Console.WriteLine($"calculator.Divide(1, 0) → Exception: {ex.Message}");
}
