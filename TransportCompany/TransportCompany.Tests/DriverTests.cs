
using TransportCompany.Domain.Data;

namespace TransportCompany.Tests;

/// <summary>
/// Проверки аналитики по водителям
/// </summary>
public class DriverTests
{
    /// <summary>
    /// Проверяет получение пяти водителей с наибольшим количеством рейсов
    /// </summary>
    [Fact]
    public void GetTopFiveDriversByTripCount()
    {
        var result = TransportData.Trips
            .GroupBy(trip => trip.Driver)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key.Id)
            .Take(5)
            .Select(group => group.Key.Id)
            .ToList();

        Assert.Equal(new[] { 3, 1, 5, 2, 7 }, result);
    }
}
