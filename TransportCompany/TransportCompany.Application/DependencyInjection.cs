
using Microsoft.Extensions.DependencyInjection;
using TransportCompany.Application.Contracts.Clients;
using TransportCompany.Application.Contracts.Drivers;
using TransportCompany.Application.Contracts.Trips;
using TransportCompany.Application.Contracts.VehicleModels;
using TransportCompany.Application.Contracts.Vehicles;
using TransportCompany.Application.Services;

namespace TransportCompany.Application;

/// <summary>
/// Предоставляет методы расширения для регистрации сервисов приложения
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Регистрирует сервисы прикладного слоя
    /// </summary>
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IDriverService, DriverService>();
        services.AddScoped<ITripService, TripService>();
        services.AddScoped<IVehicleModelService, VehicleModelService>();
        services.AddScoped<IVehicleService, VehicleService>();

        return services;
    }
}
