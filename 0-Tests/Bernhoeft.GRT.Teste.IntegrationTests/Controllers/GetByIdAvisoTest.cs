using System.Net;
using System.Text.Json;
using FluentAssertions;
using Xunit;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Controllers;
public class GetByIdAvisoTest : IClassFixture<CustomWebAPI>
{
    private const string METHOD = "api/v1/avisos";
    private readonly HttpClient _httpClient;
    public GetByIdAvisoTest(CustomWebAPI customWebApplicationFactory)
    {
        _httpClient = customWebApplicationFactory.CreateClient();
    }

    [Theory(DisplayName = "Dada uma requisição inválida para o get por id, a API deve retornar uma mensagem de erro")]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(null)]
    public async Task ErrorGetByIdRequisicaoInvalida(int id)
    {
        //Arrange
        var result = await _httpClient.GetAsync($"{METHOD}/{id}");
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        //Act
        var body = await result.Content.ReadAsStreamAsync();

        //Assert
        var response = await JsonDocument.ParseAsync(body);
        response.RootElement.GetProperty("Mensagens").EnumerateArray().Should().NotBeNullOrEmpty();
    }
    [Theory(DisplayName = "Dada uma requisição válida para o get por id e não for encontrado nenhum aviso, a API deve retornar notfound.")]
    [InlineData(500)]
    [InlineData(501)]
    public async Task AvisoNaoEncontrado(int id)
    {
        //Arrange
        var result = await _httpClient.GetAsync($"{METHOD}/{id}");
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);

    }
    [Theory(DisplayName = "Dada uma requisição válida para o get por id e for encontra o aviso, a API deve retornar o aviso.")]
    [InlineData(1)]
    [InlineData(2)]
    public async Task AvisoEncontradoComSucesso(int id)
    {
        //Arrange
        var result = await _httpClient.GetAsync($"{METHOD}/{id}");
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        //Act
        var body = await result.Content.ReadAsStreamAsync();

        //Assert
        var response = await JsonDocument.ParseAsync(body);
        response.RootElement.GetProperty("Dados").Should().NotBeNull();
    }
}
