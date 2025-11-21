using Bernhoeft.GRT.ContractWeb.Domain.SqlServer.ContractStore.Interfaces.Repositories;
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Core.Extensions;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1;
public class GetAvisoByIdHandler : IRequestHandler<GetAvisoByIdRequest, IOperationResult<GetAvisosResponse>>
{
    private readonly IServiceProvider _serviceProvider;
    private IAvisoRepository _avisoRepository => _serviceProvider.GetRequiredService<IAvisoRepository>();
    public GetAvisoByIdHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    public async Task<IOperationResult<GetAvisosResponse>> Handle(GetAvisoByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _avisoRepository.ObterAvisoPorIdAsync(request.Id,TrackingBehavior.NoTracking);

        if (result is null)
            return OperationResult<GetAvisosResponse>.ReturnNotFound();

        return OperationResult<GetAvisosResponse>.ReturnOk((GetAvisosResponse)result);
    }
}
