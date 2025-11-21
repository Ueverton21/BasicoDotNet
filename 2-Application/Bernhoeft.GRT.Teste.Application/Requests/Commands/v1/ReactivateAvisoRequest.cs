using Bernhoeft.GRT.Core.Interfaces.Results;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
public class ReactivateAvisoRequest : IRequest<IOperationResult<bool>>
{
    public int Id { get; set; }
    public ReactivateAvisoRequest(int id)
    {
        Id = id;
    }
}
