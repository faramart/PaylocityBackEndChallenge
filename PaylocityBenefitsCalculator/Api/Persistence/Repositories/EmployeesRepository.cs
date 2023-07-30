using Api.Domain.Repositories;
using Api.Models;

namespace Api.Persistence.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly InMemoryDb _db;

    public EmployeesRepository(InMemoryDb db)
    {
        _db = db;
    }

    public List<Employee> GetAll()
    {
        return _db.Employees;
    }

    public Employee? GetById(int id)
    {
        return _db.Employees.FirstOrDefault(e => e.Id == id);
    }
}