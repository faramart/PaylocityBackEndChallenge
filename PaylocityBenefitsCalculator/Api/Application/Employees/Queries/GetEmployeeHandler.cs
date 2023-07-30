using Api.Domain.Repositories;
using Api.Dtos.Employee;
using Api.Mapping;
using MediatR;

namespace Api.Application.Employees.Queries;

public class GetEmployeeHandler : IRequestHandler<GetEmployeeQuery, GetEmployeeDto?>
{
    private readonly IEmployeesRepository _employeesRepository;

    public GetEmployeeHandler(IEmployeesRepository employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }

    public Task<GetEmployeeDto?> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_employeesRepository.GetById(request.Id)?.ToDto());
    }
}