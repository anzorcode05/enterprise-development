using TransportCompany.Domain.Data;
namespace TransportCompany.Tests;
public class ClientTests
{
    public static TheoryData<int,int[]> ClientsByVehicleModelData => new()
    {
        {1,[1,3,9]}, {2,[5,2]}, {3,[5,2]}, {4,[1,4]}, {5,[1,7]}, {6,[7,4]}, {7,[1,9]}, {8,[6]}, {9,[10,8]}, {10,[3]}
    };
    [Theory][MemberData(nameof(ClientsByVehicleModelData))]
    public void GetClientsByVehicleModel(int modelId,int[] expected)
    {
        var result=TransportData.Trips.Where(t=>t.Vehicle.VehicleModel.Id==modelId).Select(t=>t.Client).Distinct().OrderBy(c=>c.Name).Select(c=>c.Id).ToList();
        Assert.Equal(expected,result);
    }
    [Fact]
    public void GetTopFiveClientsByTotalCost()
    {
        var result=TransportData.Trips.GroupBy(t=>t.Client).Select(g=>new{Client=g.Key,Cost=g.Sum(t=>t.Cost)}).OrderByDescending(x=>x.Cost).ThenBy(x=>x.Client.Name).Take(5).Select(x=>x.Client.Id).ToList();
        Assert.Equal(new[]{1,6,4,9,5},result);
    }
}
