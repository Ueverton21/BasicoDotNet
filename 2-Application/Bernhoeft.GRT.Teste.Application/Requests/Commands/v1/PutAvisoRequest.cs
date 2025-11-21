using System.Text.Json.Serialization;
using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Entities;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
public class PutAvisoRequest : IRequest<IOperationResult<GetAvisosResponse>>
{
    public string Mensagem { get; set; }
    [JsonIgnore]
    public int? Id { get; set; }
}
