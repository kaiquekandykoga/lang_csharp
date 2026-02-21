using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Calculator.WebAPI.Tests;

public class CalculatorWebApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public CalculatorWebApiTests(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task Add_ReturnsExpectedResult()
    {
        CalculatorResponse? response = await _httpClient.GetFromJsonAsync<CalculatorResponse>("/calculator/add?x=2&y=3");

        Assert.NotNull(response);
        Assert.Equal(2, response!.X);
        Assert.Equal(3, response.Y);
        Assert.Equal(5, response.Result);
    }

    [Fact]
    public async Task Subtract_ReturnsExpectedResult()
    {
        CalculatorResponse? response = await _httpClient.GetFromJsonAsync<CalculatorResponse>("/calculator/subtract?x=8&y=3");

        Assert.NotNull(response);
        Assert.Equal(8, response!.X);
        Assert.Equal(3, response.Y);
        Assert.Equal(5, response.Result);
    }

    [Fact]
    public async Task Multiply_ReturnsExpectedResult()
    {
        CalculatorResponse? response = await _httpClient.GetFromJsonAsync<CalculatorResponse>("/calculator/multiply?x=4&y=3");

        Assert.NotNull(response);
        Assert.Equal(4, response!.X);
        Assert.Equal(3, response.Y);
        Assert.Equal(12, response.Result);
    }

    [Fact]
    public async Task Divide_ReturnsExpectedResult()
    {
        CalculatorResponse? response = await _httpClient.GetFromJsonAsync<CalculatorResponse>("/calculator/divide?x=9&y=2");

        Assert.NotNull(response);
        Assert.Equal(9, response!.X);
        Assert.Equal(2, response.Y);
        Assert.Equal(4, response.Result);
    }

    [Fact]
    public async Task Divide_ByZero_ReturnsBadRequest()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/calculator/divide?x=8&y=0");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task TryDivide_WithNonZeroDivisor_ReturnsSuccessAndResult()
    {
        TryDivideResponse? response = await _httpClient.GetFromJsonAsync<TryDivideResponse>("/calculator/try-divide?x=8&y=2");

        Assert.NotNull(response);
        Assert.Equal(8, response!.X);
        Assert.Equal(2, response.Y);
        Assert.True(response.Success);
        Assert.Equal(4, response.Result);
    }

    [Fact]
    public async Task TryDivide_WithZeroDivisor_ReturnsFailureAndNullResult()
    {
        TryDivideResponse? response = await _httpClient.GetFromJsonAsync<TryDivideResponse>("/calculator/try-divide?x=8&y=0");

        Assert.NotNull(response);
        Assert.Equal(8, response!.X);
        Assert.Equal(0, response.Y);
        Assert.False(response.Success);
        Assert.Null(response.Result);
    }

    [Fact]
    public async Task Add_WithDecimalType_ReturnsExpectedResult()
    {
        DecimalCalculatorResponse? response = await _httpClient.GetFromJsonAsync<DecimalCalculatorResponse>("/calculator/add?x=2.5&y=3.5&type=decimal");

        Assert.NotNull(response);
        Assert.Equal(2.5m, response!.X);
        Assert.Equal(3.5m, response.Y);
        Assert.Equal(6.0m, response.Result);
    }

    [Fact]
    public async Task TryDivide_WithDecimalType_ReturnsExpectedResult()
    {
        DecimalTryDivideResponse? response = await _httpClient.GetFromJsonAsync<DecimalTryDivideResponse>("/calculator/try-divide?x=8.4&y=2.1&type=decimal");

        Assert.NotNull(response);
        Assert.Equal(8.4m, response!.X);
        Assert.Equal(2.1m, response.Y);
        Assert.True(response.Success);
        Assert.Equal(4.0m, response.Result);
    }

    [Fact]
    public async Task Add_WithUnsupportedType_ReturnsBadRequest()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/calculator/add?x=2&y=3&type=float");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    private sealed record CalculatorResponse(int X, int Y, int Result);

    private sealed record TryDivideResponse(int X, int Y, bool Success, int? Result);

    private sealed record DecimalCalculatorResponse(decimal X, decimal Y, decimal Result);

    private sealed record DecimalTryDivideResponse(decimal X, decimal Y, bool Success, decimal? Result);
}