using System;
using System.Collections.Generic;
using System.Threading;
using Api.Application.Employees.Queries;
using Api.Domain.Repositories;
using Api.Models;
using Moq;
using Xunit;

namespace ApiTests.UnitTests;

public class GetPaycheckPreviewHandlerTests
{
    [Fact]
    public async void SimpleCalculation_CorrectResult()
    {
        // Arrange
        var repositoryMock = GetRepositoryMock(new Employee
        {
            Id = 1,
            Salary = 50_000,
        });

        var query = new GetPaycheckPreviewQuery(1);
        var handler = new GetPaycheckPreviewHandler(repositoryMock);

        var expected = (1000*12)/26m; // Base cost per year / number of paychecks

        // Act
        var result = await handler.Handle(query, new CancellationToken());

        // Assert
        Assert.Equal(expected, result!.BenefitCostsPerPaycheck);
    }

    [Fact]
    public async void EmployeeWithHighSalary_AdditionalCostsByPercentage()
    {
        // Arrange
        var repositoryMock = GetRepositoryMock(new Employee
        {
            Id = 1,
            Salary = 100_000,
        });

        var query = new GetPaycheckPreviewQuery(1);
        var handler = new GetPaycheckPreviewHandler(repositoryMock);

        var expected = ((1000*12)+(100_000*0.02m))/26m; // Base cost per year plus additional 2% of yearly salary / number of paychecks

        // Act
        var result = await handler.Handle(query, new CancellationToken());

        // Assert
        Assert.Equal(expected, result!.BenefitCostsPerPaycheck);
    }

    [Fact]
    public async void EmployeeWithDependent_DependentCostIncluded()
    {
        // Arrange
        var repositoryMock = GetRepositoryMock(new Employee
        {
            Id = 1,
            Salary = 50_000,
            Dependents = new List<Dependent>
            {
                new Dependent
                {
                    DateOfBirth = DateTime.Today.AddYears(-15),
                }
            }
        });

        var query = new GetPaycheckPreviewQuery(1);
        var handler = new GetPaycheckPreviewHandler(repositoryMock);

        var expected = ((1000*12)+(600*12))/26m; // Base cost per year plus dependent cost per year / number of paychecks

        // Act
        var result = await handler.Handle(query, new CancellationToken());

        // Assert
        Assert.Equal(expected, result!.BenefitCostsPerPaycheck);
    }

    [Fact]
    public async void EmployeeWithOldDependent_HigherDependentCostIncluded()
    {
        // Arrange
        var repositoryMock = GetRepositoryMock(new Employee
        {
            Id = 1,
            Salary = 50_000,
            Dependents = new List<Dependent>
            {
                new Dependent
                {
                    DateOfBirth = DateTime.Today.AddYears(-51),
                }
            }
        });

        var query = new GetPaycheckPreviewQuery(1);
        var handler = new GetPaycheckPreviewHandler(repositoryMock);

        var expected = ((1000*12)+(800*12))/26m; // Base cost per year plus older dependent cost per year / number of paychecks

        // Act
        var result = await handler.Handle(query, new CancellationToken());

        // Assert
        Assert.Equal(expected, result!.BenefitCostsPerPaycheck);
    }

    [Fact]
    public async void EmployeeWithMultipleDependents_DependentsCostIncluded()
    {
        // Arrange
        var repositoryMock = GetRepositoryMock(new Employee
        {
            Id = 1,
            Salary = 50_000,
            Dependents = new List<Dependent>
            {
                new Dependent
                {
                    DateOfBirth = DateTime.Today.AddYears(-15),
                },
                new Dependent
                {
                    DateOfBirth = DateTime.Today.AddYears(-12),
                },
                new Dependent
                {
                    DateOfBirth = DateTime.Today.AddYears(-9),
                }
            }
        });

        var query = new GetPaycheckPreviewQuery(1);
        var handler = new GetPaycheckPreviewHandler(repositoryMock);

        var expected = ((1000*12)+(600*12*3))/26m; // Base cost per year plus 3 dependents cost per year / number of paychecks

        // Act
        var result = await handler.Handle(query, new CancellationToken());

        // Assert
        Assert.Equal(expected, result!.BenefitCostsPerPaycheck);
    }

    [Fact]
    public async void ComplexCalculation_CorrectResult()
    {
        // Arrange
        var repositoryMock = GetRepositoryMock(new Employee
        {
            Id = 1,
            Salary = 100_000,
            Dependents = new List<Dependent>
            {
                new Dependent
                {
                    DateOfBirth = DateTime.Today.AddYears(-15),
                },
                new Dependent
                {
                    DateOfBirth = DateTime.Today.AddYears(-12),
                },
                new Dependent
                {
                    DateOfBirth = DateTime.Today.AddYears(-51),
                }
            }
        });

        var query = new GetPaycheckPreviewQuery(1);
        var handler = new GetPaycheckPreviewHandler(repositoryMock);

        // Base cost per year plus 2 dependents cost per year plus old dependent plus high salary / number of paychecks
        var expected = ((1000*12)+(600*12*2)+(800*12)+(100_000*0.02m))/26m;

        // Act
        var result = await handler.Handle(query, new CancellationToken());

        // Assert
        Assert.Equal(expected, result!.BenefitCostsPerPaycheck);
    }

    private IEmployeesRepository GetRepositoryMock(Employee employee)
    {
        var repositoryMock = new Mock<IEmployeesRepository>();
        repositoryMock.Setup(x => x.GetById(It.IsAny<int>()))
            .Returns(employee);

        return repositoryMock.Object;
    }
}