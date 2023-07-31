using Api.Domain.Repositories;
using Api.Dtos.Dependent;
using Api.Mapping;
using MediatR;

namespace Api.Application.Dependents.Queries;

public class GetAllDependentsHandler : IRequestHandler<GetAllDependentsQuery ,IEnumerable<GetDependentDto>>
{
    private readonly IEmployeesRepository _employeesRepository;

    public GetAllDependentsHandler(IEmployeesRepository employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }

    public Task<IEnumerable<GetDependentDto>> Handle(GetAllDependentsQuery request, CancellationToken cancellationToken)
    {
        var dependents = _employeesRepository.GetAll().SelectMany(e => e.Dependents);
        var dto = dependents.Select(x => x.ToDto());

        return Task.FromResult(dto);
    }
}