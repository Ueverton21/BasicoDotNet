using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Bernhoeft.GRT.Teste.IntegrationTests.Builders;
using FluentAssertions;
using Xunit;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Controllers;
public class ReactvateAvisoTest : IClassFixture<CustomWebAPI>
{
    private const string METHOD = "api/v1/avisos";
    private readonly HttpClient _httpClient;
    public ReactvateAvisoTest(CustomWebAPI customWebApplicationFactory)
    {
        _httpClient = customWebApplicationFactory.CreateClient();
    }

    [Fact(DisplayName = "Dada uma requisição válida para reativar o aviso, a API deve editar e retornar sucesso")]
    public async Task EditarAvisoComSucesso()
    {
        //Arrange
        var result = await _httpClient.PutAsync($"{METHOD}/1/reativar",null);
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    [Theory(DisplayName = "Dada uma requisição inválida para reativar o aviso, a API deve retornar erro(BadRequest).")]
    [InlineData(null)]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task EditarAvisoProblemaNaRequisicao(int id)
    {
        //Arrange
        var result = await _httpClient.PutAsync($"{METHOD}/{id}/reativar", null);
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        //Act
        var body = await result.Content.ReadAsStreamAsync();

        //Assert
        var response = await JsonDocument.ParseAsync(body);
        response.RootElement.GetProperty("Mensagens").EnumerateArray().Should().NotBeNullOrEmpty();

    }
    [Theory(DisplayName = "Dada uma requisição válida para reativar o aviso, mas nenhum aviso for encontrado, a API deve retornar erro(NotFound).")]
    [InlineData(20)]
    [InlineData(21)]
    public async Task AvisoNaoEncontrado(int id)
    {
        //Arrange
        var result = await _httpClient.PutAsync($"{METHOD}/{id}/reativar", null);
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
