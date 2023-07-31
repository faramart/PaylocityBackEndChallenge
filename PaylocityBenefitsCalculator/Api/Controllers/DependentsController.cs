using System.ComponentModel.DataAnnotations;
using Api.Application.Dependents.Commands;
using Api.Application.Dependents.Queries;
using Api.Dtos.Dependent;
using Api.Mapping;
using Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DependentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        var dependent = await _mediator.Send(new GetDependentQuery(id));

        if (dependent == null)
        {
            return NotFound(new ApiResponse<GetDependentDto>
            {
                Success = false
            });
        }

        return new ApiResponse<GetDependentDto>
        {
            Success = true,
            Data = dependent
        };
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var query = new GetAllDependentsQuery();
        var dependents = await _mediator.Send(query);

        var result = new ApiResponse<List<GetDependentDto>>
        {
            Data = dependents.ToList(),
            Success = true
        };

        return result;
    }

    [SwaggerOperation(Summary = "Create new dependent")]
    [HttpPost("")]
    public async Task<ActionResult<ApiResponse>> Create(CreateDependentDto dto)
    {
        try
        {
            await _mediator.Send(dto.ToCommand());
            return new ApiResponse
            {
                Success = true
            };
        }
        catch (ValidationException e)
        {
            return BadRequest(new ApiResponse
            {
                Success = false,
                Error = e.Message
            });
        }
        catch (InvalidOperationException e)
        {
            return NotFound(new ApiResponse
            {
                Success = false,
                Error = e.Message
            });
        }
    }
}
