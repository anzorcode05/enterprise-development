namespace TransportCompany.Domain.Entities;

public class Driver
{
    public required int Id { get; set; }
    public required string PassportNumber { get; set; }
    public required string FullName { get; set; }
    public int Experience { get; set; }
    public required string LicenseCategory { get; set; }
}
