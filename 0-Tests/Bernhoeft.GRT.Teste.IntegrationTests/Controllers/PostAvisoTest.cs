using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Bernhoeft.GRT.Teste.IntegrationTests.Builders;
using FluentAssertions;
using Xunit;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Controllers;
public class PostAvisoTest : IClassFixture<CustomWebAPI>
{
    private const string METHOD = "api/v1/avisos";
    private readonly HttpClient _httpClient;
    public PostAvisoTest(CustomWebAPI customWebApplicationFactory)
    {
        _httpClient = customWebApplicationFactory.CreateClient();
    }

    [Fact(DisplayName = "Dada uma requisição válida para o criar aviso, a API deve criar e retornar o aviso criado.")]
    public async Task CriarAvisoComSucesso()
    {
        //Arrange
        var request = CreateAvisoRequestBuilder.Build();
        
        var result = await _httpClient.PostAsJsonAsync($"{METHOD}",request);
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        //Act
        var body = await result.Content.ReadAsStreamAsync();

        //Assert
        var response = await JsonDocument.ParseAsync(body);
        response.RootElement.GetProperty("Dados").Should().NotBeNull();
    }
    [Theory(DisplayName = "Dada uma requisição inválida para o criar aviso, a API deve rtornar erro(BadRequest).")]
    [InlineData("","Teste")]
    [InlineData("Test","")]
    [InlineData(null, "Teste")]
    [InlineData("Teste", null)]
    public async Task AvisoNaoEncontrado(string mensagem, string titulo)
    {
        var request = CreateAvisoRequestBuilder.Build();
        request.Mensagem = mensagem;
        request.Titulo = titulo;
        //Arrange
        var result = await _httpClient.PostAsJsonAsync($"{METHOD}",request);
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        //Act
        var body = await result.Content.ReadAsStreamAsync();

        //Assert
        var response = await JsonDocument.ParseAsync(body);
        response.RootElement.GetProperty("Mensagens").EnumerateArray().Should().NotBeNullOrEmpty();

    }
}
