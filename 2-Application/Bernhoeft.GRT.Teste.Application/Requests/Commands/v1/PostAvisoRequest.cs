using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1
{
    public class PostAvisoRequest : IRequest<IOperationResult<GetAvisosResponse>>
    {
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        /// <summary>
        /// Método que faz o mapeamento de PostAvisoRequest para criação do AvisoEntity
        /// </summary>
        /// <returns></returns>
        public AvisoEntity MappingCriarAviso()
        {
            return new()
            {
                Titulo = Titulo,
                Mensagem = Mensagem,
                Ativo = true,
                CriadoEm = DateTime.Now,
            };
        }
    }
}