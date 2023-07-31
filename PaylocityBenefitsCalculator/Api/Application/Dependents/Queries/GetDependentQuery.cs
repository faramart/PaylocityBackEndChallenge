using Api.Dtos.Dependent;
using MediatR;

namespace Api.Application.Dependents.Queries;

public record GetDependentQuery(int Id) : IRequest<GetDependentDto?>;