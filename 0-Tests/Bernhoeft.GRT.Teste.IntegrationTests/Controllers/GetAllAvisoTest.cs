using System.Net;
using System.Text.Json;
using FluentAssertions;
using Xunit;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Controllers;
public class GetAllAvisoTest : IClassFixture<CustomWebAPI>
{
    private const string METHOD = "api/v1/avisos";
    private readonly HttpClient _httpClient;
    public GetAllAvisoTest(CustomWebAPI customWebApplicationFactory)
    {
        _httpClient = customWebApplicationFactory.CreateClient();
    }

    [Fact(DisplayName = "Dada uma requisição válida par ao get de avisos, a API deve retornar a lista de avisos com sucesso")]
    public async Task GetAllSuccess()
    {
        //Arrange
        var result = await _httpClient.GetAsync($"{METHOD}");
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        //Act
        var body = await result.Content.ReadAsStreamAsync();

        //Assert
        var response = await JsonDocument.ParseAsync(body);
        response.RootElement.GetProperty("Dados").EnumerateArray().Should().NotBeNullOrEmpty();
    }
}
