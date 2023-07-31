using Api.Dtos.Employee;
using MediatR;

namespace Api.Application.Employees.Queries;

public record GetEmployeeQuery(int Id) : IRequest<GetEmployeeDto?>;