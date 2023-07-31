using Api.Domain.Repositories;
using Api.Dtos.Dependent;
using Api.Mapping;
using MediatR;

namespace Api.Application.Dependents.Queries;

public class GetDependentHandler : IRequestHandler<GetDependentQuery, GetDependentDto?>
{
    private readonly IEmployeesRepository _employeesRepository;

    public GetDependentHandler(IEmployeesRepository employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }

    public Task<GetDependentDto?> Handle(GetDependentQuery request, CancellationToken cancellationToken)
    {
        var dependent = _employeesRepository.GetAll().SelectMany(e => e.Dependents)
            .FirstOrDefault(d => d.Id == request.Id);

        return Task.FromResult(dependent?.ToDto());
    }
}