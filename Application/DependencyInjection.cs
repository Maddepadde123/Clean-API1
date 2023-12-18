using Infrastructure.Interfaces;
using Infrastructure.Repositories.AnimalRepository;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using Infrastructure.Repositories.AnimalRepository;
using Microsoft.Extensions.DependencyInjection;
 
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

        //services.AddValidatorsFromAssembly(assembly);

        return services;

    }

}