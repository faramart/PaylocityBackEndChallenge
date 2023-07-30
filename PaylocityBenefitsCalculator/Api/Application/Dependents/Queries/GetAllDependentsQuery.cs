using Api.Dtos.Dependent;
using MediatR;

namespace Api.Application.Dependents.Queries;

public class GetAllDependentsQuery : IRequest<IEnumerable<GetDependentDto>>
{
}