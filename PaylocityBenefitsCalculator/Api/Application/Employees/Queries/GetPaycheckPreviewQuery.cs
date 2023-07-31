using Api.Dtos.Employee;
using MediatR;

namespace Api.Application.Employees.Queries;

public record GetPaycheckPreviewQuery(int EmployeeId) : IRequest<GetPaycheckPreviewDto?>;