
using Microsoft.EntityFrameworkCore;
using TransportCompany.Domain.Entities;
using TransportCompany.Infrastructure.PostgreSql.Repositories;

namespace TransportCompany.Tests;

/// <summary>
/// Проверяет работу PostgreSQL-репозиториев с реальной базой данных
/// </summary>
public sealed class PostgreSqlRepositoryTests(
    PostgreSqlDatabaseFixture database)
    : IClassFixture<PostgreSqlDatabaseFixture>
{
    /// <summary>
    /// Проверяет создание, чтение, обновление и удаление клиента
    /// </summary>
    [Fact]
    public async Task ClientRepositorySupportsCrudAndGeneratedIds()
    {
        var cancellationToken = TestContext.Current.CancellationToken;

        await using var context = database.CreateDbContext();
        var repository = new PostgreSqlClientRepository(context);

        var client = new Client
        {
            Name = "Integration test client",
            Phone = "+7 (900) 123-45-67"
        };

        await repository.AddAsync(client, cancellationToken);

        Assert.True(client.Id > 10);

        var createdClient = await repository.GetByIdAsync(
            client.Id,
            cancellationToken);

        Assert.NotNull(createdClient);
        Assert.Equal("Integration test client", createdClient.Name);

        var updatedClient = new Client
        {
            Name = "Updated integration client",
            Phone = "+7 (900) 765-43-21"
        };

        Assert.True(await repository.UpdateAsync(
            client.Id,
            updatedClient,
            cancellationToken));

        var savedClient = await repository.GetByIdAsync(
            client.Id,
            cancellationToken);

        Assert.NotNull(savedClient);
        Assert.Equal("Updated integration client", savedClient.Name);
        Assert.Equal("+7 (900) 765-43-21", savedClient.Phone);

        Assert.True(await repository.DeleteAsync(client.Id, cancellationToken));
        Assert.Null(await repository.GetByIdAsync(client.Id, cancellationToken));
        Assert.False(await repository.DeleteAsync(client.Id, cancellationToken));
    }

    /// <summary>
    /// Проверяет получение клиентов по модели транспорта и сортировку по имени
    /// </summary>
    [Fact]
    public async Task ClientRepositoryFiltersByVehicleModelInDatabase()
    {
        var cancellationToken = TestContext.Current.CancellationToken;

        await using var context = database.CreateDbContext();
        var repository = new PostgreSqlClientRepository(context);

        var clients = await repository.GetByVehicleModelAsync(
            1,
            cancellationToken);

        Assert.Equal(new[] { 1, 3, 9 }, clients.Select(client => client.Id));
    }

    /// <summary>
    /// Проверяет группировку и сортировку клиентов по общей стоимости рейсов
    /// </summary>
    [Fact]
    public async Task ClientRepositoryCalculatesTopTotalCostsInDatabase()
    {
        var cancellationToken = TestContext.Current.CancellationToken;

        await using var context = database.CreateDbContext();
        var repository = new PostgreSqlClientRepository(context);

        var clients = await repository.GetTopByTotalCostAsync(
            5,
            cancellationToken);

        Assert.Equal(
            new[] { 1, 6, 4, 9, 5 },
            clients.Select(client => client.ClientId));
    }

    /// <summary>
    /// Проверяет группировку и сортировку водителей по количеству рейсов
    /// </summary>
    [Fact]
    public async Task DriverRepositoryCalculatesTopTripCountsInDatabase()
    {
        var cancellationToken = TestContext.Current.CancellationToken;

        await using var context = database.CreateDbContext();
        var repository = new PostgreSqlDriverRepository(context);

        var drivers = await repository.GetTopByTripCountAsync(
            5,
            cancellationToken);

        Assert.Equal(
            new[] { 3, 1, 5, 2, 7 },
            drivers.Select(driver => driver.DriverId));
    }

    /// <summary>
    /// Проверяет получение транспортных средств, участвовавших в рейсах
    /// </summary>
    [Fact]
    public async Task VehicleRepositoryReturnsVehiclesUsedOnTrips()
    {
        var cancellationToken = TestContext.Current.CancellationToken;

        await using var context = database.CreateDbContext();
        var repository = new PostgreSqlVehicleRepository(context);

        var vehicles = await repository.GetVehiclesOnTripsAsync(cancellationToken);

        Assert.Equal(
            Enumerable.Range(1, 10),
            vehicles.Select(vehicle => vehicle.Id));

        Assert.All(
            vehicles,
            vehicle => Assert.NotNull(vehicle.VehicleModel));
    }

    /// <summary>
    /// Проверяет подсчёт рейсов для каждого транспортного средства в базе данных
    /// </summary>
    [Fact]
    public async Task VehicleRepositoryCalculatesTripCountsInDatabase()
    {
        var cancellationToken = TestContext.Current.CancellationToken;

        await using var context = database.CreateDbContext();
        var repository = new PostgreSqlVehicleRepository(context);

        var summaries = await repository.GetTripCountsAsync(cancellationToken);

        Assert.Equal(
            Enumerable.Range(1, 10),
            summaries.Select(summary => summary.VehicleId));

        Assert.Equal(
            new[] { 3, 2, 2, 2, 2, 2, 2, 2, 2, 1 },
            summaries.Select(summary => summary.TripCount));
    }

    /// <summary>
    /// Проверяет получение рейсов клиента и водителя вместе со связанными сущностями
    /// </summary>
    [Fact]
    public async Task TripRepositoryLoadsRelatedEntitiesForFilteredTrips()
    {
        var cancellationToken = TestContext.Current.CancellationToken;

        await using var context = database.CreateDbContext();
        var repository = new PostgreSqlTripRepository(context);

        var clientTrips = await repository.GetByClientIdAsync(
            1,
            cancellationToken);

        var driverTrips = await repository.GetByDriverIdAsync(
            1,
            cancellationToken);

        Assert.Equal(
            new[] { 1, 6, 9, 16 },
            clientTrips.OrderBy(trip => trip.Id).Select(trip => trip.Id));

        Assert.Equal(
            new[] { 1, 2, 3 },
            driverTrips.OrderBy(trip => trip.Id).Select(trip => trip.Id));

        Assert.All(clientTrips, trip =>
        {
            Assert.NotNull(trip.Driver);
            Assert.NotNull(trip.Vehicle.VehicleModel);
            Assert.NotNull(trip.Client);
        });
    }

    /// <summary>
    /// Проверяет наличие связанных записей для ограничений удаления
    /// </summary>
    [Fact]
    public async Task RepositoriesDetectRelatedRecords()
    {
        var cancellationToken = TestContext.Current.CancellationToken;

        await using var context = database.CreateDbContext();

        var clients = new PostgreSqlClientRepository(context);
        var drivers = new PostgreSqlDriverRepository(context);
        var vehicles = new PostgreSqlVehicleRepository(context);
        var models = new PostgreSqlVehicleModelRepository(context);

        Assert.True(await clients.HasTripsAsync(1, cancellationToken));
        Assert.True(await drivers.HasTripsAsync(1, cancellationToken));
        Assert.True(await vehicles.HasTripsAsync(1, cancellationToken));
        Assert.True(await models.HasVehiclesAsync(1, cancellationToken));
    }

    /// <summary>
    /// Проверяет, что внешние ключи запрещают удаление клиента с рейсами
    /// </summary>
    [Fact]
    public async Task ClientRepositoryCannotDeleteClientUsedByTrips()
    {
        var cancellationToken = TestContext.Current.CancellationToken;

        await using var context = database.CreateDbContext();
        var repository = new PostgreSqlClientRepository(context);

        await Assert.ThrowsAsync<DbUpdateException>(
            () => repository.DeleteAsync(1, cancellationToken));
    }
}
