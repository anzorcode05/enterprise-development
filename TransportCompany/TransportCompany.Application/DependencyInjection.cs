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
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Регистрирует сервисы прикладного слоя
        /// </summary>
        /// <returns>Коллекция зарегистрированных сервисов</returns>
        public IServiceCollection AddApplication()
        {
            services.AddSingleton<IClientService, ClientService>();
            services.AddSingleton<IDriverService, DriverService>();
            services.AddSingleton<ITripService, TripService>();
            services.AddSingleton<IVehicleModelService, VehicleModelService>();
            services.AddSingleton<IVehicleService, VehicleService>();

            return services;
        }
    }
}