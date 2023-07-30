using Api.Domain.Repositories;
using Api.Dtos.Dependent;
using Api.Mapping;
using MediatR;

namespace Api.Application.Dependents.Queries;

public class GetDependentHandler : IRequestHandler<GetDependentQuery, GetDependentDto?>
{
    private readonly IDependentsRepository _dependentsRepository;

    public GetDependentHandler(IDependentsRepository dependentsRepository)
    {
        _dependentsRepository = dependentsRepository;
    }

    public Task<GetDependentDto?> Handle(GetDependentQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dependentsRepository.GetById(request.Id)?.ToDto());
    }
}