using Api.Dtos.Employee;
using MediatR;

namespace Api.Application.Employees.Queries;

public class GetAllEmployeesQuery : IRequest<IEnumerable<GetEmployeeDto>>
{
}