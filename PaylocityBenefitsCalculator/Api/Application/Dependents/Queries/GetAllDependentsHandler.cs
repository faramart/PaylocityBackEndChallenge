using Api.Domain.Repositories;
using Api.Dtos.Dependent;
using Api.Mapping;
using MediatR;

namespace Api.Application.Dependents.Queries;

public class GetAllDependentsHandler : IRequestHandler<GetAllDependentsQuery ,IEnumerable<GetDependentDto>>
{
    private readonly IDependentsRepository _dependentsRepository;

    public GetAllDependentsHandler(IDependentsRepository dependentsRepository)
    {
        _dependentsRepository = dependentsRepository;
    }

    public Task<IEnumerable<GetDependentDto>> Handle(GetAllDependentsQuery request, CancellationToken cancellationToken)
    {
        var dependents = _dependentsRepository.GetAll();
        var dtos = dependents.Select(x => x.ToDto());

        return Task.FromResult(dtos);
    }
}