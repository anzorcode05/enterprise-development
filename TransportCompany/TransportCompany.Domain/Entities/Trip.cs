namespace TransportCompany.Domain.Entities;

public class Trip
{
    public required int Id { get; set; }
    public required Driver Driver { get; set; }
    public required Vehicle Vehicle { get; set; }
    public required Client Client { get; set; }
    public required string Origin { get; set; }
    public required string Destination { get; set; }
    public DateOnly DepartureDate { get; set; }
    public double CargoWeight { get; set; }
    public decimal Cost { get; set; }
}
