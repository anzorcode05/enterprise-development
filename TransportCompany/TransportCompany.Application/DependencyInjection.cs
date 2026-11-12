
using Microsoft.Extensions.DependencyInjection;
using TransportCompany.Application.Contracts.Clients;
using TransportCompany.Application.Contracts.Drivers;
using TransportCompany.Application.Contracts.Trips;
using TransportCompany.Application.Contracts.VehicleModels;
using TransportCompany.Application.Contracts.Vehicles;
using TransportCompany.Application.Services;

namespace TransportCompany.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddSingleton<IClientService, ClientService>();
        services.AddSingleton<IDriverService, DriverService>();
        services.AddSingleton<ITripService, TripService>();
        services.AddSingleton<IVehicleModelService, VehicleModelService>();
        services.AddSingleton<IVehicleService, VehicleService>();

        return services;
    }
}
