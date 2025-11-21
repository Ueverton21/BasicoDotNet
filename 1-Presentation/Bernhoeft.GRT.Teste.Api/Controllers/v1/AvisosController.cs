using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Api.Validator;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations;
using Bernhoeft.GRT.Teste.Application.Responses.Errors;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using FluentValidation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bernhoeft.GRT.Teste.Api.Controllers.v1
{
    /// <summary>
    /// Gerencia os avisos do sistema.
    /// </summary>
    /// <remarks>
    /// Endpoints responsáveis por consultar, criar, atualizar e remover avisos.
    /// </remarks>
    /// <response code="401">Não autenticado.</response>
    /// <response code="403">Não autorizado.</response>
    /// <response code="500">Erro interno no servidor.</response>
    [AllowAnonymous]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = null)]
    public class AvisosController : RestApiController
    {
        /// <summary>
        /// Retorna um Aviso por ID.
        /// </summary>
        /// <param name="id">Id do aviso</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Aviso.</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="400">Dados da request inválidos.</response>
        /// <response code="404">Aviso não Encontrado.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAvisosResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<object> GetAviso([FromRoute] int id, CancellationToken cancellationToken)
        {
            //Garantir que a validação ocorra antes de chegar na camada de aplicação
            var request = new GetAvisoByIdRequest(id);

            var errors = FluentValidationErrors.Validate(new GetAvisoByIdRequestValidator().Validate(request));
            if (errors != null)
                return BadRequest(errors);

            return await Mediator.Send(request, cancellationToken);
        }

        /// <summary>
        /// Retorna Todos os Avisos(que estão ativos) Cadastrados para Tela de Edição.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>Lista com Todos os Avisos.</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="204">Sem Avisos.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetAvisosResponse>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<object> GetAvisos(CancellationToken cancellationToken)
            => await Mediator.Send(new GetAvisosRequest(), cancellationToken);

        /// <summary>
        /// Criar um novo aviso.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Aviso.</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="400">Dados Inválidos.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAvisosResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<object> PostAviso([FromBody] PostAvisoRequest request, CancellationToken cancellationToken)
        {
            return await Mediator.Send(request, cancellationToken);
        }
        /// <summary>
        /// Editar um aviso.
        /// </summary>
        /// <param name="id">Id do aviso</param>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Aviso.</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="400">Dados Inválidos.</response>
        /// <response code="404">Aviso não Encontrado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAvisosResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<object> PutAviso([FromRoute] int id,[FromBody] PutAvisoRequest request, CancellationToken cancellationToken)
        {
            //Garantir que a validação ocorra antes de chegar na camada de aplicação
            request.Id = id;
            
            var errors = FluentValidationErrors.Validate(new PutAvisoRequestValidator().Validate(request));
            if (errors != null)
                return BadRequest(errors); 

            return await Mediator.Send(request, cancellationToken);
        }
        /// <summary>
        /// Exclusão lógica de um aviso.
        /// </summary>
        /// <param name="id">Id do aviso</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Aviso.</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="400">Dados Inválidos.</response>
        /// <response code="404">Aviso não Encontrado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<object> DeleteAviso([FromRoute] int id, CancellationToken cancellationToken)
        {
            //Garantir que a validação ocorra antes de chegar na camada de aplicação
            var request = new DeleteAvisoRequest(id);

            var errors = FluentValidationErrors.Validate(new DeleteAvisoRequestValidator().Validate(request));
            if (errors != null)
                return BadRequest(errors);

            return await Mediator.Send(request, cancellationToken);
        }
    }
}