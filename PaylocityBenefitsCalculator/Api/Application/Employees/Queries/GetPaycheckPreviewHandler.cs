using Api.Domain.Repositories;
using Api.Dtos.Employee;
using Api.Models;
using MediatR;

namespace Api.Application.Employees.Queries;

public class GetPaycheckPreviewHandler : IRequestHandler<GetPaycheckPreviewQuery, GetPaycheckPreviewDto?>
{
    private readonly IEmployeesRepository _employeesRepository;

    // TODO Could be stored in a configuration file
    private const int NumberOfPaychecksPerYear = 26;
    private const int BaseCost = 1_000;
    private const int BaseDependentCost = 600;
    private const int BaseOlderDependentCost = 200;
    private const int OlderDependentThreshold = 50;
    private const decimal AdditionalBenefitCostPercentage = 0.02m;
    private const int AdditionalBenefitCostThreshold = 80_000;

    public GetPaycheckPreviewHandler(IEmployeesRepository employeesRepository)
    {
        _employeesRepository = employeesRepository;
    }

    public Task<GetPaycheckPreviewDto?> Handle(GetPaycheckPreviewQuery request, CancellationToken cancellationToken)
    {
        var employee = _employeesRepository.GetById(request.EmployeeId);
        if (employee == null)
        {
            return Task.FromResult<GetPaycheckPreviewDto?>(null);
        }

        var result = new GetPaycheckPreviewDto
        {
            EmployeeId = employee.Id,
            BenefitCostsPerPaycheck = GetCostOfBenefitsPerPaycheck(employee)
        };

        return Task.FromResult(result)!;
    }

    // TODO Calculation logic could be moved to a dedicated service
    private static decimal GetCostOfBenefitsPerPaycheck(Employee employee)
    {
        decimal costPerMonth = 0;

        costPerMonth += BaseCost;
        costPerMonth += GetCostOfDependentsPerMonth(employee.Dependents);

        var costPerYear = costPerMonth * 12;
        if (employee.Salary > AdditionalBenefitCostThreshold)
        {
            costPerYear += employee.Salary * AdditionalBenefitCostPercentage;
        }

        return costPerYear / NumberOfPaychecksPerYear;
    }

    private static int GetCostOfDependentsPerMonth(IEnumerable<Dependent> dependents)
    {
        var result = 0;

        foreach (var dependent in dependents)
        {
            result += BaseDependentCost;

            if (GetAgeInYears(dependent.DateOfBirth) > OlderDependentThreshold)
            {
                result += BaseOlderDependentCost;
            }
        }

        return result;
    }


    /// TODO Should be moved to dedicated service or helper class
    /// <summary>
    /// https://stackoverflow.com/questions/9/how-do-i-calculate-someones-age-based-on-a-datetime-type-birthday
    /// </summary>
    private static int GetAgeInYears(DateTime dateOfBirth)
    {
        // Save today's date.
        var today = DateTime.Today;

        // Calculate the age.
        var age = today.Year - dateOfBirth.Year;

        // Go back to the year in which the person was born in case of a leap year
        if (dateOfBirth.Date > today.AddYears(-age)) age--;

        return age;
    }
}