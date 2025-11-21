using Bernhoeft.GRT.Teste.Application.Responses.Errors;
using FluentValidation.Results;

namespace Bernhoeft.GRT.Teste.Api.Validator;

public static class FluentValidationErrors
{
    /// <summary>
    /// Método para converter erros de validação do FluentValidation em um ErrorResponse
    /// </summary>
    /// <param name="validation"></param>
    /// <returns></returns>
    public static ErrorResponse Validate(ValidationResult validation)
    {
        if (!validation.IsValid)
        {
            ErrorResponse errors = new();
            errors.Mensagens = validation.Errors.Select(x => x.ErrorMessage).ToList();
            return errors;
        }
        return null;
    }
}
