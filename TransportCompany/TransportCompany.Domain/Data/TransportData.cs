using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Shared;

namespace TransportCompany.Domain.Data;

/// <summary>
/// Тестовые данные транспортной компании
/// </summary>
public static class TransportData
{
    /// <summary>
    /// Список моделей транспортных средств
    /// </summary>
    public static IReadOnlyList<VehicleModel> VehicleModels { get; } =
    [
        new VehicleModel { Id = 1, BodyType = BodyType.Tent, BodyVolume = 60 },
        new VehicleModel { Id = 2, BodyType = BodyType.Refrigerator, BodyVolume = 40 },
        new VehicleModel { Id = 3, BodyType = BodyType.Tank, BodyVolume = 30 },
        new VehicleModel { Id = 4, BodyType = BodyType.Flatbed, BodyVolume = 80 },
        new VehicleModel { Id = 5, BodyType = BodyType.Container, BodyVolume = 70 },
        new VehicleModel { Id = 6, BodyType = BodyType.Isothermal, BodyVolume = 50 },
        new VehicleModel { Id = 7, BodyType = BodyType.Tent, BodyVolume = 90 },
        new VehicleModel { Id = 8, BodyType = BodyType.Refrigerator, BodyVolume = 35 },
        new VehicleModel { Id = 9, BodyType = BodyType.Container, BodyVolume = 100 },
        new VehicleModel { Id = 10, BodyType = BodyType.Flatbed, BodyVolume = 75 }
    ];

    /// <summary>
    /// Список транспортных средств
    /// </summary>
    public static IReadOnlyList<Vehicle> Vehicles { get; } =
    [
        new Vehicle { Id = 1,  LicensePlate = "AA1234A", Brand = "Volvo",  LoadCapacity = 20, VehicleModel = VehicleModels[0] },
        new Vehicle { Id = 2,  LicensePlate = "BB2345B", Brand = "Scania", LoadCapacity = 25, VehicleModel = VehicleModels[1] },
        new Vehicle { Id = 3,  LicensePlate = "CC3456C", Brand = "MAN", LoadCapacity = 15, VehicleModel = VehicleModels[2] },
        new Vehicle { Id = 4,  LicensePlate = "DD4567D", Brand = "KAMAZ", LoadCapacity = 30, VehicleModel = VehicleModels[3] },
        new Vehicle { Id = 5,  LicensePlate = "EE5678E", Brand = "GAZ", LoadCapacity = 10, VehicleModel = VehicleModels[4] },
        new Vehicle { Id = 6,  LicensePlate = "FF6789F", Brand = "DAF", LoadCapacity = 22, VehicleModel = VehicleModels[5] },
        new Vehicle { Id = 7,  LicensePlate = "GG7890G", Brand = "Volvo", LoadCapacity = 18, VehicleModel = VehicleModels[6] },
        new Vehicle { Id = 8,  LicensePlate = "HH8901H", Brand = "Scania", LoadCapacity = 27, VehicleModel = VehicleModels[7] },
        new Vehicle { Id = 9,  LicensePlate = "II9012I", Brand = "MAN", LoadCapacity = 35, VehicleModel = VehicleModels[8] },
        new Vehicle { Id = 10, LicensePlate = "JJ0123J", Brand = "KAMAZ", LoadCapacity = 12, VehicleModel = VehicleModels[9] }
    ];

    /// <summary>
    /// Список водителей
    /// </summary>
    public static IReadOnlyList<Driver> Drivers { get; } =
    [
        new Driver { Id = 1,  PassportNumber = "4501 111111", FullName = "Ivanov Ivan Ivanovich", Experience = 15, LicenseCategory = "CE" },
        new Driver { Id = 2,  PassportNumber = "4502 222222", FullName = "Petrov Petr Petrovich", Experience = 8,  LicenseCategory = "C"  },
        new Driver { Id = 3,  PassportNumber = "4503 333333", FullName = "Sidorov Alexey Sergeevich", Experience = 20, LicenseCategory = "CE" },
        new Driver { Id = 4,  PassportNumber = "4504 444444", FullName = "Kuznetsov Dmitry Andreevich", Experience = 5,  LicenseCategory = "C"  },
        new Driver { Id = 5,  PassportNumber = "4505 555555", FullName = "Smirnov Nikolay Pavlovich", Experience = 12, LicenseCategory = "CE" },
        new Driver { Id = 6,  PassportNumber = "4506 666666", FullName = "Popov Sergey Vladimirovich", Experience = 3,  LicenseCategory = "B"  },
        new Driver { Id = 7,  PassportNumber = "4507 777777", FullName = "Fedorov Andrey Olegovich", Experience = 18, LicenseCategory = "CE" },
        new Driver { Id = 8,  PassportNumber = "4508 888888", FullName = "Morozov Pavel Igorevich", Experience = 7,  LicenseCategory = "C"  },
        new Driver { Id = 9,  PassportNumber = "4509 999999", FullName = "Volkov Oleg Viktorovich", Experience = 25, LicenseCategory = "CE" },
        new Driver { Id = 10, PassportNumber = "4510 101010", FullName = "Orlov Maxim Yurievich", Experience = 10, LicenseCategory = "C"  }
    ];

    /// <summary>
    /// Список клиентов
    /// </summary>
    public static IReadOnlyList<Client> Clients { get; } =
    [
        new Client { Id = 1,  Name = "Ivanov Ivan Ivanovich", Phone = "+7 (900) 111-11-11" },
        new Client { Id = 2,  Name = "Petrova Anna Sergeevna",  Phone = "+7 (900) 222-22-22" },
        new Client { Id = 3,  Name = "OOO Romashka",  Phone = "+7 (900) 333-33-33" },
        new Client { Id = 4,  Name = "Sidorov Alexey Petrovich",Phone = "+7 (900) 444-44-44" },
        new Client { Id = 5,  Name = "AO Zvezda",  Phone = "+7 (900) 555-55-55" },
        new Client { Id = 6,  Name = "Kuznetsova Maria Ivanovna",Phone = "+7 (900) 666-66-66" },
        new Client { Id = 7,  Name = "OOO Vektor",  Phone = "+7 (900) 777-77-77" },
        new Client { Id = 8,  Name = "Smirnov Nikolay Pavlovich",Phone = "+7 (900) 888-88-88" },
        new Client { Id = 9,  Name = "Popova Elena Vladimirovna",Phone = "+7 (900) 999-99-99" },
        new Client { Id = 10, Name = "PAO Sever", Phone = "+7 (900) 000-00-00" }
    ];

    /// <summary>
    /// Список рейсов
    /// </summary>
    public static IReadOnlyList<Trip> Trips { get; } =
    [
         
        new Trip { Id = 1,  Driver = Drivers[0], Vehicle = Vehicles[0], Client = Clients[0], Origin = "Moscow", Destination = "SPb",DepartureDate = new DateOnly(2026, 1, 10), CargoWeight = 15, Cost = 80000  },
        new Trip { Id = 2,  Driver = Drivers[0], Vehicle = Vehicles[0], Client = Clients[2], Origin = "Moscow", Destination = "Kazan", DepartureDate = new DateOnly(2026, 2, 5),  CargoWeight = 12, Cost = 60000  },
        new Trip { Id = 3,  Driver = Drivers[0], Vehicle = Vehicles[1], Client = Clients[4], Origin = "SPb", Destination = "Novgorod",DepartureDate = new DateOnly(2026, 3, 12), CargoWeight = 18, Cost = 90000  },

        new Trip { Id = 4,  Driver = Drivers[1], Vehicle = Vehicles[2], Client = Clients[1], Origin = "Kazan", Destination = "Moscow", DepartureDate = new DateOnly(2026, 1, 20), CargoWeight = 10, Cost = 50000  },
        new Trip { Id = 5,  Driver = Drivers[1], Vehicle = Vehicles[3], Client = Clients[3], Origin = "Moscow", Destination = "Sochi",  DepartureDate = new DateOnly(2026, 4, 1),  CargoWeight = 25, Cost = 120000 },

        new Trip { Id = 6,  Driver = Drivers[2], Vehicle = Vehicles[4], Client = Clients[0], Origin = "Moscow", Destination = "Rostov",  DepartureDate = new DateOnly(2026, 1, 15), CargoWeight = 8,  Cost = 45000  },
        new Trip { Id = 7,  Driver = Drivers[2], Vehicle = Vehicles[5], Client = Clients[6], Origin = "Rostov", Destination = "Moscow",  DepartureDate = new DateOnly(2026, 2, 20), CargoWeight = 20, Cost = 100000 },
        new Trip { Id = 8,  Driver = Drivers[2], Vehicle = Vehicles[0], Client = Clients[8], Origin = "Moscow", Destination = "Tver",    DepartureDate = new DateOnly(2026, 3, 5),  CargoWeight = 14, Cost = 70000  },
        new Trip { Id = 9,  Driver = Drivers[2], Vehicle = Vehicles[6], Client = Clients[0], Origin = "Tver", Destination = "Moscow",  DepartureDate = new DateOnly(2026, 4, 10), CargoWeight = 16, Cost = 85000  },

        new Trip { Id = 10, Driver = Drivers[3], Vehicle = Vehicles[7], Client = Clients[5], Origin = "Moscow",Destination = "Omsk",    DepartureDate = new DateOnly(2026, 2, 1),  CargoWeight = 22, Cost = 110000 },

        new Trip { Id = 11, Driver = Drivers[4], Vehicle = Vehicles[8], Client = Clients[9], Origin = "Omsk", Destination = "Moscow",  DepartureDate = new DateOnly(2026, 1, 25), CargoWeight = 30, Cost = 150000 },
        new Trip { Id = 12, Driver = Drivers[4], Vehicle = Vehicles[8], Client = Clients[7], Origin = "Moscow", Destination = "Ufa",     DepartureDate = new DateOnly(2026, 3, 18), CargoWeight = 28, Cost = 140000 },
        new Trip { Id = 13, Driver = Drivers[4], Vehicle = Vehicles[9], Client = Clients[2], Origin = "Ufa", Destination = "Moscow",  DepartureDate = new DateOnly(2026, 4, 22), CargoWeight = 11, Cost = 55000  },

        new Trip { Id = 14, Driver = Drivers[5], Vehicle = Vehicles[1], Client = Clients[1], Origin = "Moscow", Destination = "Kursk",   DepartureDate = new DateOnly(2026, 2, 14), CargoWeight = 9,  Cost = 48000  },

        new Trip { Id = 15, Driver = Drivers[6], Vehicle = Vehicles[2], Client = Clients[4], Origin = "Kursk",  Destination = "Moscow",  DepartureDate = new DateOnly(2026, 3, 1),  CargoWeight = 13, Cost = 65000  },
        new Trip { Id = 16, Driver = Drivers[6], Vehicle = Vehicles[3], Client = Clients[0], Origin = "Moscow",  Destination = "Voronezh",DepartureDate = new DateOnly(2026, 4, 5),  CargoWeight = 17, Cost = 90000  },

        new Trip { Id = 17, Driver = Drivers[7], Vehicle = Vehicles[4], Client = Clients[6], Origin = "Voronezh",Destination = "Moscow",  DepartureDate = new DateOnly(2026, 1, 30), CargoWeight = 7,  Cost = 40000  },

        new Trip { Id = 18, Driver = Drivers[8], Vehicle = Vehicles[5], Client = Clients[3], Origin = "Moscow",  Destination = "Murmansk",DepartureDate = new DateOnly(2026, 2, 25), CargoWeight = 21, Cost = 105000 },
        new Trip { Id = 19, Driver = Drivers[8], Vehicle = Vehicles[6], Client = Clients[8], Origin = "Murmansk",Destination = "Moscow",  DepartureDate = new DateOnly(2026, 4, 15), CargoWeight = 19, Cost = 98000  },

        new Trip { Id = 20, Driver = Drivers[9], Vehicle = Vehicles[7], Client = Clients[5], Origin = "Moscow", Destination = "Yaroslavl",DepartureDate = new DateOnly(2026, 3, 22), CargoWeight = 24, Cost = 115000 }
    ];
}