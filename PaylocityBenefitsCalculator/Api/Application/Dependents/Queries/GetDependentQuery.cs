using Api.Dtos.Dependent;
using MediatR;

namespace Api.Application.Dependents.Queries;

public class GetDependentQuery : IRequest<GetDependentDto?>
{
    public int Id { get; }

    public GetDependentQuery(int id)
    {
        Id = id;
    }
}