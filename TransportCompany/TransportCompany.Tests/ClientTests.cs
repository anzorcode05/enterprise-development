
using TransportCompany.Domain.Data;

namespace TransportCompany.Tests;

/// <summary>
/// Проверки аналитики по клиентам
/// </summary>
public class ClientTests
{
    /// <summary>
    /// Данные для проверки получения клиентов по модели транспорта
    /// </summary>
    public static TheoryData<int, int[]> ClientsByVehicleModelData => new()
    {
        { 1, [1, 3, 9] },
        { 2, [5, 2] },
        { 3, [5, 2] },
        { 4, [1, 4] },
        { 5, [1, 7] },
        { 6, [7, 4] },
        { 7, [1, 9] },
        { 8, [6] },
        { 9, [10, 8] },
        { 10, [3] }
    };

    /// <summary>
    /// Проверяет получение клиентов, использующих указанную модель транспорта
    /// </summary>
    [Theory]
    [MemberData(nameof(ClientsByVehicleModelData))]
    public void GetClientsByVehicleModel(int modelId, int[] expected)
    {
        var result = TransportData.Trips
            .Where(trip => trip.Vehicle.VehicleModel.Id == modelId)
            .Select(trip => trip.Client)
            .Distinct()
            .OrderBy(client => client.Name)
            .Select(client => client.Id)
            .ToList();

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Проверяет получение пяти клиентов с наибольшей общей стоимостью перевозок
    /// </summary>
    [Fact]
    public void GetTopFiveClientsByTotalCost()
    {
        var result = TransportData.Trips
            .GroupBy(trip => trip.Client)
            .Select(group => new
            {
                Client = group.Key,
                Cost = group.Sum(trip => trip.Cost)
            })
            .OrderByDescending(item => item.Cost)
            .ThenBy(item => item.Client.Name)
            .Take(5)
            .Select(item => item.Client.Id)
            .ToList();

        Assert.Equal(new[] { 1, 6, 4, 9, 5 }, result);
    }
}
