using TransportCompany.Domain.Data;

namespace TransportCompany.Tests;

/// <summary>
/// Тесты для операций с транспортными средствами
/// </summary>
public class VehicleTests
{
    /// <summary>
    /// Проверяет получение транспортных средств, находящихся в рейсе
    /// </summary>
    [Fact]
    public void GetVehiclesOnTrip()
    {
        var expected = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        var result = TransportData.Trips
            .Select(trip => trip.Vehicle.Id)
            .Distinct()
            .OrderBy(id => id)
            .ToList();

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Проверяет количество рейсов для каждого транспортного средства
    /// </summary>
    [Theory]
    [InlineData(1, 3)]
    [InlineData(2, 2)]
    [InlineData(3, 2)]
    [InlineData(4, 2)]
    [InlineData(5, 2)]
    [InlineData(6, 2)]
    [InlineData(7, 2)]
    [InlineData(8, 1)]
    [InlineData(9, 1)]
    [InlineData(10, 1)]
    public void GetTripCountForEachVehicle(int vehicleId, int expected)
    {
        var result = TransportData.Trips
            .Count(trip => trip.Vehicle.Id == vehicleId);

        Assert.Equal(expected, result);
    }
}