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
}