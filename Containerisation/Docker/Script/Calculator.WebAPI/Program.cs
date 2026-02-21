using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();
var calcGroup = app.MapGroup("/calculator")
    .WithTags("Calculator");

void MapBinary(string pattern, OperationType op, string name) =>
    calcGroup.MapGet(pattern, ([FromQuery] string x, [FromQuery] string y, [FromQuery] string? type) =>
        CalculatorEndpointHandlers.DispatchBinaryOperation(x, y, type, op))
    .WithName(name);

MapBinary("/add", OperationType.Add, "Add");
MapBinary("/subtract", OperationType.Subtract, "Subtract");
MapBinary("/multiply", OperationType.Multiply, "Multiply");
MapBinary("/divide", OperationType.Divide, "Divide");

calcGroup.MapGet("/try-divide", ([FromQuery] string x, [FromQuery] string y, [FromQuery] string? type) =>
    CalculatorEndpointHandlers.DispatchTryDivideOperation(x, y, type))
.WithName("TryDivide");

app.Run();
