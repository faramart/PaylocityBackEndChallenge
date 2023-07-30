using Api.Application.Employees.Queries;
using Api.Dtos.Employee;
using Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        var employee = await _mediator.Send(new GetEmployeeQuery(id));

        if (employee == null)
        {
            return NotFound(new ApiResponse<GetEmployeeDto>
            {
                Success = false
            });
        }

        return new ApiResponse<GetEmployeeDto>
        {
            Success = true,
            Data = employee
        };
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        var query = new GetAllEmployeesQuery();
        var employees = await _mediator.Send(query);

        var result = new ApiResponse<List<GetEmployeeDto>>
        {
            Data = employees.ToList(),
            Success = true
        };

        return result;
    }
}
