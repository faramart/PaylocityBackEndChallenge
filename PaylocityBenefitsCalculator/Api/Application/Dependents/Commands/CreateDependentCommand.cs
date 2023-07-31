using Api.Models;
using MediatR;

namespace Api.Application.Dependents.Commands;

public record CreateDependentCommand(
    int EmployeeId,
    int Id,
    string? FirstName,
    string? LastName,
    DateTime DateOfBirth,
    Relationship Relationship) : IRequest;