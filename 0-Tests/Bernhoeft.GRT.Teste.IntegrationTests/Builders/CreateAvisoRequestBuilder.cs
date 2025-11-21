using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bogus;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Builders;
public class CreateAvisoRequestBuilder
{
    public static PostAvisoRequest Build()
    {
        return new Faker<PostAvisoRequest>()
            .RuleFor(a => a.Titulo, f => f.Lorem.Sentence(5))
            .RuleFor(a => a.Mensagem, f => f.Lorem.Paragraph())
            .Generate();
    }
}
