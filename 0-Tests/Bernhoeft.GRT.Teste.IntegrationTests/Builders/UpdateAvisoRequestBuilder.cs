using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bogus;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Builders;
public class UpdateAvisoRequestBuilder
{
    public static PutAvisoRequest Build()
    {
        return new Faker<PutAvisoRequest>()
            .RuleFor(a => a.Id, f => 1)
            .RuleFor(a => a.Mensagem, f => f.Lorem.Paragraph())
            .Generate();
    }
}
