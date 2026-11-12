using Microsoft.Extensions.DependencyInjection;
using TransportCompany.Domain.Data;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Repositories;

namespace TransportCompany.Infrastructure.InMemory;

/// <summary>
/// Предоставляет методы расширения для регистрации хранилища в памяти
/// </summary>
public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Регистрирует репозитории транспортной компании
        /// </summary>
        /// <returns>Коллекция зарегистрированных сервисов</returns>
        public IServiceCollection AddInMemoryStorage()
        {
            services.AddSingleton<IRepository<Client>>(
                _ => new InMemoryRepository<Client>(TransportData.Clients, x => x.Id));

            services.AddSingleton<IRepository<Driver>>(
                _ => new InMemoryRepository<Driver>(TransportData.Drivers, x => x.Id));

            services.AddSingleton<IRepository<VehicleModel>>(
                _ => new InMemoryRepository<VehicleModel>(TransportData.VehicleModels, x => x.Id));

            services.AddSingleton<IRepository<Vehicle>>(
                _ => new InMemoryRepository<Vehicle>(TransportData.Vehicles, x => x.Id));

            services.AddSingleton<IRepository<Trip>>(
                _ => new InMemoryRepository<Trip>(TransportData.Trips, x => x.Id));

            return services;
        }
    }
}