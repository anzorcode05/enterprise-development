
using TransportCompany.Domain.Data;

namespace TransportCompany.Tests;

/// <summary>
/// Проверки аналитики по транспортным средствам
/// </summary>
public class VehicleTests
{
    /// <summary>
    /// Проверяет получение списка транспортных средств, участвовавших в рейсах
    /// </summary>
    [Fact]
    public void GetVehiclesOnTrip()
    {
        var result = TransportData.Trips
            .Select(trip => trip.Vehicle.Id)
            .Distinct()
            .OrderBy(id => id)
            .ToList();

        Assert.Equal(Enumerable.Range(1, 10), result);
    }

    /// <summary>
    /// Проверяет подсчёт количества рейсов для каждого транспортного средства
    /// </summary>
    [Theory]
    [InlineData(1, 3)]
    [InlineData(2, 2)]
    [InlineData(3, 2)]
    [InlineData(4, 2)]
    [InlineData(5, 2)]
    [InlineData(6, 2)]
    [InlineData(7, 2)]
    [InlineData(8, 2)]
    [InlineData(9, 2)]
    [InlineData(10, 1)]
    public void GetTripCountForEachVehicle(int id, int expected)
    {
        var result = TransportData.Trips.Count(trip => trip.Vehicle.Id == id);

        Assert.Equal(expected, result);
    }
}
