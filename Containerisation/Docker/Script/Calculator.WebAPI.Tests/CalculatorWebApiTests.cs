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
    public async Task Divide_ByZero_ReturnsBadRequest()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/calculator/divide?x=8&y=0");

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    private sealed record CalculatorResponse(int X, int Y, int Result);
}