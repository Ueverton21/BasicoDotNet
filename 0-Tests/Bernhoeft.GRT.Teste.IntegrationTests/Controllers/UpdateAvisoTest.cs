using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Bernhoeft.GRT.Teste.IntegrationTests.Builders;
using FluentAssertions;
using Xunit;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Controllers;
public class UpdateAvisoTest : IClassFixture<CustomWebAPI>
{
    private const string METHOD = "api/v1/avisos";
    private readonly HttpClient _httpClient;
    public UpdateAvisoTest(CustomWebAPI customWebApplicationFactory)
    {
        _httpClient = customWebApplicationFactory.CreateClient();
    }

    [Fact(DisplayName = "Dada uma requisição válida para o editar aviso, a API deve editar e retornar o aviso editado.")]
    public async Task EditarAvisoComSucesso()
    {
        //Arrange
        var request = UpdateAvisoRequestBuilder.Build();

        var result = await _httpClient.PutAsJsonAsync($"{METHOD}/{request.Id}", request);
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        //Act
        var body = await result.Content.ReadAsStreamAsync();

        //Assert
        var response = await JsonDocument.ParseAsync(body);
        response.RootElement.GetProperty("Dados").Should().NotBeNull();
    }
    [Theory(DisplayName = "Dada uma requisição inválida para editar o aviso, a API deve retornar erro(BadRequest).")]
    [InlineData("Test", null)]
    [InlineData("Test", -1)]
    [InlineData("",1)]
    [InlineData(null, 1)]
    public async Task EditarAvisoProblemaNaRequisicao(string mensagem, int id)
    {
        var request = UpdateAvisoRequestBuilder.Build();
        request.Mensagem = mensagem;
        request.Id = id;
        //Arrange
        var result = await _httpClient.PutAsJsonAsync($"{METHOD}/{id}", request);
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        //Act
        var body = await result.Content.ReadAsStreamAsync();

        //Assert
        var response = await JsonDocument.ParseAsync(body);
        response.RootElement.GetProperty("Mensagens").EnumerateArray().Should().NotBeNullOrEmpty();

    }
    [Theory(DisplayName = "Dada uma requisição válida para editar aviso, mas nenhum aviso for encontrado, a API deve retornar erro(NotFound).")]
    [InlineData(20)]
    [InlineData(21)]
    public async Task AvisoNaoEncontrado(int id)
    {
        var request = UpdateAvisoRequestBuilder.Build();
        request.Id = id;
        //Arrange
        var result = await _httpClient.PutAsJsonAsync($"{METHOD}/{id}", request);
        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
