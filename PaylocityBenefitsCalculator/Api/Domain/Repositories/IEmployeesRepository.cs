using Api.Models;

namespace Api.Domain.Repositories;

public interface IEmployeesRepository
{
    List<Employee> GetAll();

    Employee? GetById(int id);
}