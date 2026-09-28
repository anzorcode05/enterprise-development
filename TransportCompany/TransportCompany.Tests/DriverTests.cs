using TransportCompany.Domain.Data;

namespace TransportCompany.Tests;

/// <summary>
/// Тесты для операций с водителями
/// </summary>
public class DriverTests
{
    /// <summary>
    /// Проверяет получение пяти водителей с наибольшим количеством рейсов
    /// </summary>
    [Fact]
    public void GetTopFiveDriversByTripCount()
    {
        var expected = new List<int> { 3, 1, 5, 2, 7 };

        var result = TransportData.Trips
            .GroupBy(trip => trip.Driver)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key.Id)
            .Take(5)
            .Select(group => group.Key.Id)
            .ToList();

        Assert.Equal(expected, result);
    }
}