using CalculatorLib = Calculator.Library.Calculator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
var calcGroup = app.MapGroup("/calculator")
    .WithTags("Calculator");

calcGroup.MapGet("/add", (int x, int y) =>
{
    int result = CalculatorLib.Add(x, y);
    return Results.Ok(new CalculatorResponse(x, y, result));
})
.WithName("Add");

calcGroup.MapGet("/subtract", (int x, int y) =>
{
    int result = CalculatorLib.Subtract(x, y);
    return Results.Ok(new CalculatorResponse(x, y, result));
})
.WithName("Subtract");

calcGroup.MapGet("/multiply", (int x, int y) =>
{
    int result = CalculatorLib.Multiply(x, y);
    return Results.Ok(new CalculatorResponse(x, y, result));
})
.WithName("Multiply");

calcGroup.MapGet("/divide", (int x, int y) =>
{
    try
    {
        int result = CalculatorLib.Divide(x, y);
        return Results.Ok(new CalculatorResponse(x, y, result));
    }
    catch (DivideByZeroException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
})
.WithName("Divide");

calcGroup.MapGet("/try-divide", (int x, int y) =>
{
    bool success = CalculatorLib.TryDivide(x, y, out int result);

    return Results.Ok(new TryDivideResponse(x, y, success, success ? result : null));
})
.WithName("TryDivide");

app.Run();

record CalculatorResponse(int X, int Y, int Result);

record TryDivideResponse(int X, int Y, bool Success, int? Result);
