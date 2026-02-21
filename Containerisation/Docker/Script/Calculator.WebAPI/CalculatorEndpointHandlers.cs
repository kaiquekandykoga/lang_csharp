using System.Globalization;
using System.Numerics;
using CalculatorLib = Calculator.Library.Calculator;

internal static class CalculatorEndpointHandlers
{
    internal static IResult DispatchBinaryOperation(string x, string y, string? type, OperationType operation)
    {
        string normalizedType = NormalizeNumberType(type);

        return normalizedType switch
        {
            "int" => HandleBinaryOperation<int>(x, y, operation),
            "long" => HandleBinaryOperation<long>(x, y, operation),
            "double" => HandleBinaryOperation<double>(x, y, operation),
            "decimal" => HandleBinaryOperation<decimal>(x, y, operation),
            _ => Results.BadRequest(new { error = "Unsupported type. Supported types: int, long, double, decimal." })
        };
    }

    internal static IResult DispatchTryDivideOperation(string x, string y, string? type)
    {
        string normalizedType = NormalizeNumberType(type);

        return normalizedType switch
        {
            "int" => HandleTryDivideOperation<int>(x, y),
            "long" => HandleTryDivideOperation<long>(x, y),
            "double" => HandleTryDivideOperation<double>(x, y),
            "decimal" => HandleTryDivideOperation<decimal>(x, y),
            _ => Results.BadRequest(new { error = "Unsupported type. Supported types: int, long, double, decimal." })
        };
    }

    private static IResult HandleBinaryOperation<T>(string xRaw, string yRaw, OperationType operation)
        where T : struct, INumber<T>, IParsable<T>
    {
        if (!TryParseNumber(xRaw, out T x) || !TryParseNumber(yRaw, out T y))
        {
            return Results.BadRequest(new { error = "Invalid numeric value for selected type." });
        }

        try
        {
            T result = operation switch
            {
                OperationType.Add => CalculatorLib.Add(x, y),
                OperationType.Subtract => CalculatorLib.Subtract(x, y),
                OperationType.Multiply => CalculatorLib.Multiply(x, y),
                OperationType.Divide => CalculatorLib.Divide(x, y),
                _ => throw new NotSupportedException("Unsupported operation.")
            };

            return Results.Ok(new CalculatorResponse<T>(x, y, result));
        }
        catch (DivideByZeroException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
        catch (OverflowException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static IResult HandleTryDivideOperation<T>(string xRaw, string yRaw)
        where T : struct, INumber<T>, IParsable<T>
    {
        if (!TryParseNumber(xRaw, out T x) || !TryParseNumber(yRaw, out T y))
        {
            return Results.BadRequest(new { error = "Invalid numeric value for selected type." });
        }

        bool success = CalculatorLib.TryDivide(x, y, out T result);
        return Results.Ok(new TryDivideResponse<T>(x, y, success, success ? result : null));
    }

    private static bool TryParseNumber<T>(string raw, out T value)
        where T : struct, IParsable<T>
        => T.TryParse(raw, CultureInfo.InvariantCulture, out value);

    private static string NormalizeNumberType(string? type)
        => string.IsNullOrWhiteSpace(type) ? "int" : type.Trim().ToLowerInvariant();
}

internal enum OperationType
{
    Add,
    Subtract,
    Multiply,
    Divide
}

internal record CalculatorResponse<T>(T X, T Y, T Result);

internal record TryDivideResponse<T>(T X, T Y, bool Success, T? Result) where T : struct;
