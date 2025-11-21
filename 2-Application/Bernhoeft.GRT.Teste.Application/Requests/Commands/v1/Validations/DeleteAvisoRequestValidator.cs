using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;
public class DeleteAvisoRequestValidator : AbstractValidator<DeleteAvisoRequest>
{
    public DeleteAvisoRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("O Id não pode ser nulo.")
            .GreaterThan(0).WithMessage("O Id deve ser maior que zero.");
    }
}
