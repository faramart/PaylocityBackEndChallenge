using Api.Dtos.Employee;
using MediatR;

namespace Api.Application.Employees.Queries;

public class GetPaycheckPreviewQuery : IRequest<GetPaycheckPreviewDto?>
{
    public int EmployeeId { get; }

    public GetPaycheckPreviewQuery(int employeeId)
    {
        EmployeeId = employeeId;
    }
}