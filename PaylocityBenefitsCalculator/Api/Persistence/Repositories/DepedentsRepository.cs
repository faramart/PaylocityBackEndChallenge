using Api.Domain.Repositories;
using Api.Models;

namespace Api.Persistence.Repositories;

public class DependentsRepository : IDependentsRepository
{
    private readonly InMemoryDb _db;

    public DependentsRepository(InMemoryDb db)
    {
        _db = db;
    }

    public List<Dependent> GetAll()
    {
        return _db.Employees.SelectMany(e => e.Dependents).ToList();
    }

    public Dependent? GetById(int id)
    {
        return GetAll().FirstOrDefault(d => d.Id == id);
    }
}