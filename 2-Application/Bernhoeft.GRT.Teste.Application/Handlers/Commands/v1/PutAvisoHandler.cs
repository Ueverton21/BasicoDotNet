using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1;
public class PutAvisoHandler : IRequestHandler<PutAvisoRequest, IOperationResult<GetAvisosResponse>>
{
    private readonly IServiceProvider _serviceProvider;
    private IContext _context => _serviceProvider.GetRequiredService<IContext>();
    private IAvisoRepository _avisoRepository => _serviceProvider.GetRequiredService<IAvisoRepository>();

    public PutAvisoHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;
    public async Task<IOperationResult<GetAvisosResponse>> Handle(PutAvisoRequest request, CancellationToken cancellationToken)
    {
        var aviso = await _avisoRepository.ObterAvisoPorIdAsync((int)request.Id, TrackingBehavior.NoTracking);

        if (aviso is null)
            return OperationResult<GetAvisosResponse>.ReturnNotFound();

        aviso.Mensagem = request.Mensagem;
        aviso.EditadoEm = DateTime.Now;

        _avisoRepository.Update(aviso);
        await _context.SaveChangesAsync(cancellationToken);

        return OperationResult<GetAvisosResponse>.ReturnOk((GetAvisosResponse)aviso);
    }
}
