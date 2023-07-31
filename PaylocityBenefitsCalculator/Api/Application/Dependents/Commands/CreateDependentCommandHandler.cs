using System.ComponentModel.DataAnnotations;
using Api.Domain.Repositories;
using Api.Mapping;
using Api.Models;
using MediatR;

namespace Api.Application.Dependents.Commands;

public class CreateDependentCommandHandler : IRequestHandler<CreateDependentCommand>
{
    private readonly IEmployeesRepository _employeesRepository;

    public CreateDependentCommandHandler(IEmployeesRepository employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }

    public Task Handle(CreateDependentCommand request, CancellationToken cancellationToken)
    {
        var employee = _employeesRepository.GetById(request.EmployeeId);
        if (employee == null)
        {
            // TODO Custom EmployeeNotFoundException would be better here
            throw new InvalidOperationException($"Employee with id {request.EmployeeId} not found");
        }

        if (request.Relationship != Relationship.Child)
        {
            if (employee.Dependents.Any(d => d.Relationship is Relationship.Spouse or Relationship.DomesticPartner))
            {
                throw new ValidationException($"Employee can have only one spouse or domestic partner");
            }
        }

        employee.Dependents.Add(request.ToModel());

        return Task.CompletedTask;
    }
}