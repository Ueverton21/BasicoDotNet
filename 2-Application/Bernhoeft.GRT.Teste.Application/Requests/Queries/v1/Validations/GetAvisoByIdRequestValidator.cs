using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations;
public class GetAvisoByIdRequestValidator : AbstractValidator<GetAvisoByIdRequest>
{
    public GetAvisoByIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("O Id não pode ser nulo.")
            .GreaterThan(0).WithMessage("O Id deve ser maior que zero.");
    }
}
