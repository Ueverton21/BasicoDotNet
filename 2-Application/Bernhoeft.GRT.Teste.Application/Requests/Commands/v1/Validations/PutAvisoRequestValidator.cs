using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;
public class PutAvisoRequestValidator : AbstractValidator<PutAvisoRequest>
{
    public PutAvisoRequestValidator()
    {
        RuleFor(x => x.Mensagem)
            .NotEmpty().WithMessage("A mensagem não pode ser vazia.")
            .NotNull().WithMessage("A mensagem não pode ser nula.");
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("O ID do aviso deve ser maior que zero.");
    }
}
