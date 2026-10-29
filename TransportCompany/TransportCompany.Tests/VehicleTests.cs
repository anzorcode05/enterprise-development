using TransportCompany.Domain.Data;
namespace TransportCompany.Tests;
public class VehicleTests
{
    [Fact] public void GetVehiclesOnTrip()
    {
        var result=TransportData.Trips.Select(t=>t.Vehicle.Id).Distinct().OrderBy(id=>id).ToList();
        Assert.Equal(Enumerable.Range(1,10),result);
    }
    [Theory][InlineData(1,3)][InlineData(2,2)][InlineData(3,2)][InlineData(4,2)][InlineData(5,2)][InlineData(6,2)][InlineData(7,2)][InlineData(8,2)][InlineData(9,2)][InlineData(10,1)]
    public void GetTripCountForEachVehicle(int id,int expected) => Assert.Equal(expected,TransportData.Trips.Count(t=>t.Vehicle.Id==id));
}
