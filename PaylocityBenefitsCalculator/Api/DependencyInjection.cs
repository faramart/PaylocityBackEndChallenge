using Api.Domain.Repositories;
using Api.Persistence;
using Api.Persistence.Repositories;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<InMemoryDb>();

        services.AddMediatR((configuration) =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddScoped<IEmployeesRepository, EmployeesRepository>();
        services.AddScoped<IDependentsRepository, DependentsRepository>();

        return services;
    }
}