
using Microsoft.EntityFrameworkCore;
using TransportCompany.Domain.Data;
using TransportCompany.Domain.Entities;

namespace TransportCompany.Infrastructure.PostgreSql;

/// <summary>
/// Контекст базы данных транспортной компании
/// </summary>
public sealed class TransportCompanyDbContext(
    DbContextOptions<TransportCompanyDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Клиенты транспортной компании
    /// </summary>
    public DbSet<Client> Clients => Set<Client>();

    /// <summary>
    /// Водители транспортной компании
    /// </summary>
    public DbSet<Driver> Drivers => Set<Driver>();

    /// <summary>
    /// Модели транспортных средств
    /// </summary>
    public DbSet<VehicleModel> VehicleModels => Set<VehicleModel>();

    /// <summary>
    /// Транспортные средства
    /// </summary>
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    /// <summary>
    /// Рейсы транспортной компании
    /// </summary>
    public DbSet<Trip> Trips => Set<Trip>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureClients(modelBuilder);
        ConfigureDrivers(modelBuilder);
        ConfigureVehicleModels(modelBuilder);
        ConfigureVehicles(modelBuilder);
        ConfigureTrips(modelBuilder);
    }

    private static void ConfigureClients(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("clients");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityByDefaultColumn();

            entity.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(x => x.Phone)
                .HasColumnName("phone")
                .HasMaxLength(40)
                .IsRequired();

            entity.HasData(TransportData.Clients);
        });
    }

    private static void ConfigureDrivers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Driver>(entity =>
        {
            entity.ToTable("drivers");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityByDefaultColumn();

            entity.Property(x => x.LicenseNumber)
                .HasColumnName("license_number")
                .HasMaxLength(10)
                .IsRequired();

            entity.HasIndex(x => x.LicenseNumber)
                .IsUnique();

            entity.Property(x => x.FullName)
                .HasColumnName("full_name")
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(x => x.Experience)
                .HasColumnName("experience")
                .IsRequired();

            entity.Property(x => x.LicenseCategory)
                .HasColumnName("license_category")
                .HasConversion<string>()
                .HasMaxLength(2)
                .IsRequired();

            entity.HasData(TransportData.Drivers);
        });
    }

    private static void ConfigureVehicleModels(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VehicleModel>(entity =>
        {
            entity.ToTable("vehicle_models");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityByDefaultColumn();

            entity.Property(x => x.BodyType)
                .HasColumnName("body_type")
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(x => x.BodyVolume)
                .HasColumnName("body_volume")
                .IsRequired();

            entity.HasData(TransportData.VehicleModels);
        });
    }

    private static void ConfigureVehicles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.ToTable("vehicles");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityByDefaultColumn();

            entity.Property(x => x.LicensePlate)
                .HasColumnName("license_plate")
                .HasMaxLength(20)
                .IsRequired();

            entity.HasIndex(x => x.LicensePlate)
                .IsUnique();

            entity.Property(x => x.Brand)
                .HasColumnName("brand")
                .HasMaxLength(80)
                .IsRequired();

            entity.Property(x => x.LoadCapacity)
                .HasColumnName("load_capacity")
                .IsRequired();

            entity.Property(x => x.VehicleModelId)
                .HasColumnName("vehicle_model_id")
                .IsRequired();

            entity.HasOne(x => x.VehicleModel)
                .WithMany()
                .HasForeignKey(x => x.VehicleModelId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(
                TransportData.Vehicles.Select(vehicle => new
                {
                    vehicle.Id,
                    vehicle.LicensePlate,
                    vehicle.Brand,
                    vehicle.LoadCapacity,
                    VehicleModelId = vehicle.VehicleModel.Id
                }));
        });
    }

    private static void ConfigureTrips(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trip>(entity =>
        {
            entity.ToTable("trips");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .UseIdentityByDefaultColumn();

            entity.Property(x => x.DriverId)
                .HasColumnName("driver_id")
                .IsRequired();

            entity.Property(x => x.VehicleId)
                .HasColumnName("vehicle_id")
                .IsRequired();

            entity.Property(x => x.ClientId)
                .HasColumnName("client_id")
                .IsRequired();

            entity.HasOne(x => x.Driver)
                .WithMany()
                .HasForeignKey(x => x.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Vehicle)
                .WithMany()
                .HasForeignKey(x => x.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Client)
                .WithMany()
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(x => x.Origin)
                .HasColumnName("origin")
                .HasMaxLength(120)
                .IsRequired();

            entity.Property(x => x.Destination)
                .HasColumnName("destination")
                .HasMaxLength(120)
                .IsRequired();

            entity.Property(x => x.DepartureDate)
                .HasColumnName("departure_date")
                .IsRequired();

            entity.Property(x => x.CargoWeight)
                .HasColumnName("cargo_weight")
                .IsRequired();

            entity.Property(x => x.Cost)
                .HasColumnName("cost")
                .HasPrecision(12, 2)
                .IsRequired();

            entity.HasData(
                TransportData.Trips.Select(trip => new
                {
                    trip.Id,
                    DriverId = trip.Driver.Id,
                    VehicleId = trip.Vehicle.Id,
                    ClientId = trip.Client.Id,
                    trip.Origin,
                    trip.Destination,
                    trip.DepartureDate,
                    trip.CargoWeight,
                    trip.Cost
                }));
        });
    }
}
