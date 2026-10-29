using TransportCompany.Domain.Data;
namespace TransportCompany.Tests;
public class DriverTests
{
    [Fact] public void GetTopFiveDriversByTripCount()
    {
        var result=TransportData.Trips.GroupBy(t=>t.Driver).OrderByDescending(g=>g.Count()).ThenBy(g=>g.Key.Id).Take(5).Select(g=>g.Key.Id).ToList();
        Assert.Equal(new[]{3,1,5,2,7},result);
    }
}
