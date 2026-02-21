using CalculatorLib = Calculator.Library.Calculator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/calculator/add", (int x, int y) =>
{
    int result = CalculatorLib.Add(x, y);
    return Results.Ok(new CalculatorResponse(x, y, result));
})
.WithName("Add");

app.MapGet("/calculator/subtract", (int x, int y) =>
{
    int result = CalculatorLib.Subtract(x, y);
    return Results.Ok(new CalculatorResponse(x, y, result));
})
.WithName("Subtract");

app.MapGet("/calculator/multiply", (int x, int y) =>
{
    int result = CalculatorLib.Multiply(x, y);
    return Results.Ok(new CalculatorResponse(x, y, result));
})
.WithName("Multiply");

app.MapGet("/calculator/divide", (int x, int y) =>
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

app.MapGet("/calculator/try-divide", (int x, int y) =>
{
    bool success = CalculatorLib.TryDivide(x, y, out int result);

    return Results.Ok(new TryDivideResponse(x, y, success, success ? result : null));
})
.WithName("TryDivide");

app.Run();

public partial class Program
{
}

record CalculatorResponse(int X, int Y, int Result);

record TryDivideResponse(int X, int Y, bool Success, int? Result);
