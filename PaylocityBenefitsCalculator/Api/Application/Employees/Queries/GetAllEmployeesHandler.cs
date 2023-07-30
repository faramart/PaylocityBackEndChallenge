using Api.Domain.Repositories;
using Api.Dtos.Employee;
using Api.Mapping;
using MediatR;

namespace Api.Application.Employees.Queries;

public class GetAllEmployeesHandler : IRequestHandler<GetAllEmployeesQuery ,IEnumerable<GetEmployeeDto>>
{
    private readonly IEmployeesRepository _employeesRepository;

    public GetAllEmployeesHandler(IEmployeesRepository employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }

    public Task<IEnumerable<GetEmployeeDto>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        var employees = _employeesRepository.GetAll();
        var dtos = employees.Select(x => x.ToDto());

        return Task.FromResult(dtos);
    }
}