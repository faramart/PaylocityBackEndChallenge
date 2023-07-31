using Api.Application.Dependents.Commands;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;

namespace Api.Mapping;

public static class MappingExtensions
{
    public static GetEmployeeDto ToDto(this Employee model)
    {
        return new GetEmployeeDto
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Salary = model.Salary,
            DateOfBirth = model.DateOfBirth,
            Dependents = model.Dependents.Select(dep => dep.ToDto()).ToList()
        };
    }

    public static GetDependentDto ToDto(this Dependent model)
    {
        return new GetDependentDto()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            DateOfBirth = model.DateOfBirth,
            Relationship = model.Relationship
        };
    }

    public static CreateDependentCommand ToCommand(this CreateDependentDto dto)
    {
        return new CreateDependentCommand(dto.EmployeeId,
            dto.Id,
            dto.FirstName,
            dto.LastName,
            dto.DateOfBirth,
            dto.Relationship);
    }

    public static Dependent ToModel(this CreateDependentCommand command)
    {
        return new Dependent
        {
            Id = command.Id,
            FirstName = command.FirstName,
            LastName = command.LastName,
            DateOfBirth = command.DateOfBirth,
            Relationship = command.Relationship,
            EmployeeId = command.EmployeeId
        };
    }
}