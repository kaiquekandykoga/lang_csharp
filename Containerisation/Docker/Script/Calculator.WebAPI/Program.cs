using System.Globalization;
using System.Numerics;
using CalculatorLib = Calculator.Library.Calculator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
var calcGroup = app.MapGroup("/calculator")
    .WithTags("Calculator");

calcGroup.MapGet("/add", (string x, string y, string? type) =>
    DispatchBinaryOperation(x, y, type, OperationType.Add))
.WithName("Add");

calcGroup.MapGet("/subtract", (string x, string y, string? type) =>
    DispatchBinaryOperation(x, y, type, OperationType.Subtract))
.WithName("Subtract");

calcGroup.MapGet("/multiply", (string x, string y, string? type) =>
    DispatchBinaryOperation(x, y, type, OperationType.Multiply))
.WithName("Multiply");

calcGroup.MapGet("/divide", (string x, string y, string? type) =>
    DispatchBinaryOperation(x, y, type, OperationType.Divide))
.WithName("Divide");

calcGroup.MapGet("/try-divide", (string x, string y, string? type) =>
    DispatchTryDivideOperation(x, y, type))
.WithName("TryDivide");

app.Run();

/// <summary>
/// Dispatches the binary operation based on the provided type and operation.
/// </summary>
/// <param name="x">The first operand as a string.</param>
/// <param name="y">The second operand as a string.</param>
/// <param name="type">The numeric type to use (e.g., int, long, double, decimal). If null or empty, defaults to int.</param>
/// <param name="operation">The type of operation to perform (add, subtract, multiply, divide).</param>
/// <returns>An IResult containing the operation result or an error message.</returns>
static IResult DispatchBinaryOperation(string x, string y, string? type, OperationType operation)
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

/// <summary>
/// Dispatches the TryDivide operation based on the provided type.
/// </summary>
/// <param name="x">The dividend as a string.</param>
/// <param name="y">The divisor as a string.</param>
/// <param name="type">The numeric type to use (e.g., int, long, double, decimal). If null or empty, defaults to int.</param>
/// <returns>An IResult containing the TryDivide result or an error message.</returns>
static IResult DispatchTryDivideOperation(string x, string y, string? type)
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

/// <summary>
/// Handles the binary operation for the specified numeric type.
/// </summary> <typeparam name="T">The numeric type.</typeparam>
/// <param name="xRaw">The first operand as a raw string.</param>
/// <param name="yRaw">The second operand as a raw string.</param>
/// <param name="operation">The type of operation to perform.</param>
/// <returns>An IResult containing the operation result or an error message.</returns>
static IResult HandleBinaryOperation<T>(string xRaw, string yRaw, OperationType operation)
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

/// <summary>
/// Handles the TryDivide operation for the specified numeric type.
/// </summary>
/// <typeparam name="T">The numeric type.</typeparam>
/// <param name="xRaw">The dividend as a raw string.</param>
/// <param name="yRaw">The divisor as a raw string.</param>
/// <returns>An IResult containing the TryDivide result or an error message.</returns>
static IResult HandleTryDivideOperation<T>(string xRaw, string yRaw)
    where T : struct, INumber<T>, IParsable<T>
{
    if (!TryParseNumber(xRaw, out T x) || !TryParseNumber(yRaw, out T y))
    {
        return Results.BadRequest(new { error = "Invalid numeric value for selected type." });
    }

    bool success = CalculatorLib.TryDivide(x, y, out T result);
    return Results.Ok(new TryDivideResponse<T>(x, y, success, success ? result : null));
}

static bool TryParseNumber<T>(string raw, out T value)
    where T : struct, IParsable<T>
    => T.TryParse(raw, CultureInfo.InvariantCulture, out value);

static string NormalizeNumberType(string? type)
    => string.IsNullOrWhiteSpace(type) ? "int" : type.Trim().ToLowerInvariant();

enum OperationType
{
    Add,
    Subtract,
    Multiply,
    Divide
}

record CalculatorResponse<T>(T X, T Y, T Result);

record TryDivideResponse<T>(T X, T Y, bool Success, T? Result) where T : struct;
