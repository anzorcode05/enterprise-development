using TransportCompany.Domain.Shared;

namespace TransportCompany.Domain.Entities;

public class VehicleModel
{
    public required int Id { get; set; }
    public BodyType BodyType { get; set; }
    public double BodyVolume { get; set; }
}
