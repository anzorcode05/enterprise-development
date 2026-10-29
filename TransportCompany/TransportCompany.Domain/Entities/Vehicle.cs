namespace TransportCompany.Domain.Entities;

public class Vehicle
{
    public required int Id { get; set; }
    public required string LicensePlate { get; set; }
    public required string Brand { get; set; }
    public double LoadCapacity { get; set; }
    public required VehicleModel VehicleModel { get; set; }
}
