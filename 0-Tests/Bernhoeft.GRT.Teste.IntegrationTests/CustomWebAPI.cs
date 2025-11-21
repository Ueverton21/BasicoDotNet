using Microsoft.AspNetCore.Mvc.Testing;

namespace Bernhoeft.GRT.Teste.IntegrationTests;
public class CustomWebAPI : WebApplicationFactory<Program>
{
    /*Normalmente aqui eu sobrescreveria o método ConfigureWebHost para configurar serviços de teste, como
    por exemplo um banco de dados em memória ou mocks de serviços externos. Mas como a API já utiliza um banco
    de dados SQLite em memória para os testes de integração, não há necessidade de adicionar outro. 
    */
}
