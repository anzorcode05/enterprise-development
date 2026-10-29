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
    /// Проверяет получение пяти клиентов с наибольшей суммой затрат на перевозки.
    /// Суммы посчитаны вручную: клиент 1 - 300000, клиенты 4 и 6 - по 225000
    /// (при равенстве первым идёт клиент с меньшим ФИО), клиент 9 - 168000, клиент 5 - 155000
    /// </summary>
    [Fact]
    public void GetTopFiveClientsByTotalCost()
    {
        var expected = new List<int> { 1, 6, 4, 9, 5 };

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