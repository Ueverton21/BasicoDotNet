using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;
public class PostAvisoRequestValidator : AbstractValidator<PostAvisoRequest>
{
    public PostAvisoRequestValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("O título do aviso é obrigatório.")
            .NotNull().WithMessage("O título do aviso é obrigatório.")
            .MaximumLength(50).WithMessage("O título do aviso não pode exceder 50 caracteres.");
        RuleFor(x => x.Mensagem)
            .NotEmpty().WithMessage("A mensagem do aviso é obrigatória.")
            .NotNull().WithMessage("A mensagem do aviso é obrigatória.");
    }
}
