using Api.Models;

namespace Api.Domain.Repositories;

public interface IDependentsRepository
{
    List<Dependent> GetAll();

    Dependent? GetById(int id);
}