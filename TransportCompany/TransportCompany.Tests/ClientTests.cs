using TransportCompany.Domain.Data;

namespace TransportCompany.Tests;

/// <summary>
/// Тесты для операций с клиентами транспортной компании
/// </summary>
public class ClientTests
{
    /// <summary>
    /// Данные для проверки получения клиентов, заказывавших перевозку
    /// на транспорте указанной модели, с сортировкой по ФИО
    /// </summary>
    public static TheoryData<int, int[]> ClientsByVehicleModelData => new()
    {
        { 1, [1, 3, 5, 7] },
        { 2, [1, 3, 4, 8] },
        { 3, [3, 6, 9] },
        { 4, [2, 7] }
    };

    /// <summary>
    /// Проверяет получение клиентов, заказывавших перевозку на транспорте
    /// указанной модели, с сортировкой по ФИО
    /// </summary>
    [Theory]
    [MemberData(nameof(ClientsByVehicleModelData))]
    public void GetClientsByVehicleModel(int vehicleModelId, int[] expected)
    {
        var result = TransportData.Trips
            .Where(trip => trip.Vehicle.VehicleModel.Id == vehicleModelId)
            .Select(trip => trip.Client)
            .Distinct()
            .OrderBy(client => client.Name)
            .Select(client => client.Id)
            .ToList();

        Assert.Equal(expected, result);
    }

    /// <summary>
    /// Проверяет получение пяти клиентов с наибольшей суммой затрат на перевозки
    /// </summary>
    [Fact]
    public void GetTopFiveClientsByTotalCost()
    {
        var expected = new List<int> { 1, 9, 5, 7, 3 };

        var result = TransportData.Trips
            .GroupBy(trip => trip.Client)
            .Select(group => new
            {
                Client = group.Key,
                TotalCost = group.Sum(trip => trip.Cost)
            })
            .OrderByDescending(item => item.TotalCost)
            .ThenBy(item => item.Client.Name)
            .Take(5)
            .Select(item => item.Client.Id)
            .ToList();

        Assert.Equal(expected, result);
    }
}