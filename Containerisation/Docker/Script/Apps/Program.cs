using Apps;

var calculator = new Calculator();
int result;

result = calculator.Add(1, 1);
Console.WriteLine($"calculator.Add(1, 1) → {result}");
result = calculator.Add(1, 0);
Console.WriteLine($"calculator.Add(1, 0) → {result}");

result = calculator.Subtract(1, 1);
Console.WriteLine($"calculator.Subtract(1, 1) → {result}");

result = calculator.Multiply(1, 1);
Console.WriteLine($"calculator.Multiply(1, 1) → {result}");

try
{
    result = calculator.Divide(1, 0);
    Console.WriteLine($"calculator.Divide(1, 0) → {result}");
}
catch (DivideByZeroException ex)
{
    Console.WriteLine($"calculator.Divide(1, 0) → Exception: {ex.Message}");
}
