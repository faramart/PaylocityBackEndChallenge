using Api.Dtos.Employee;
using MediatR;

namespace Api.Application.Employees.Queries;

public class GetEmployeeQuery : IRequest<GetEmployeeDto?>
{
    public int Id { get; }

    public GetEmployeeQuery(int id)
    {
        Id = id;
    }
}