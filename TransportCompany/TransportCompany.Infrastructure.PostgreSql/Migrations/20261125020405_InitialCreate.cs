using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TransportCompany.Infrastructure.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    phone = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "drivers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    license_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    full_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    experience = table.Column<int>(type: "integer", nullable: false),
                    license_category = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drivers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_models",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    body_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    body_volume = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_models", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    license_plate = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    brand = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    load_capacity = table.Column<double>(type: "double precision", nullable: false),
                    vehicle_model_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.id);
                    table.ForeignKey(
                        name: "FK_vehicles_vehicle_models_vehicle_model_id",
                        column: x => x.vehicle_model_id,
                        principalTable: "vehicle_models",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "trips",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    driver_id = table.Column<int>(type: "integer", nullable: false),
                    vehicle_id = table.Column<int>(type: "integer", nullable: false),
                    client_id = table.Column<int>(type: "integer", nullable: false),
                    origin = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    destination = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    departure_date = table.Column<DateOnly>(type: "date", nullable: false),
                    cargo_weight = table.Column<double>(type: "double precision", nullable: false),
                    cost = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trips", x => x.id);
                    table.ForeignKey(
                        name: "FK_trips_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_trips_drivers_driver_id",
                        column: x => x.driver_id,
                        principalTable: "drivers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_trips_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalTable: "vehicles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "clients",
                columns: new[] { "id", "name", "phone" },
                values: new object[,]
                {
                    { 1, "Ivanov Ivan Ivanovich", "+7 (900) 111-11-11" },
                    { 2, "Petrova Anna Sergeevna", "+7 (900) 222-22-22" },
                    { 3, "OOO Romashka", "+7 (900) 333-33-33" },
                    { 4, "Sidorov Alexey Petrovich", "+7 (900) 444-44-44" },
                    { 5, "AO Zvezda", "+7 (900) 555-55-55" },
                    { 6, "Kuznetsova Maria Ivanovna", "+7 (900) 666-66-66" },
                    { 7, "OOO Vektor", "+7 (900) 777-77-77" },
                    { 8, "Smirnov Nikolay Pavlovich", "+7 (900) 888-88-88" },
                    { 9, "Popova Elena Vladimirovna", "+7 (900) 999-99-99" },
                    { 10, "PAO Sever", "+7 (900) 000-00-00" }
                });

            migrationBuilder.InsertData(
                table: "drivers",
                columns: new[] { "id", "experience", "full_name", "license_category", "license_number" },
                values: new object[,]
                {
                    { 1, 15, "Ivanov Ivan Ivanovich", "CE", "4501111111" },
                    { 2, 8, "Petrov Petr Petrovich", "C", "4502222222" },
                    { 3, 20, "Sidorov Alexey Sergeevich", "CE", "4503333333" },
                    { 4, 5, "Kuznetsov Dmitry Andreevich", "C", "4504444444" },
                    { 5, 12, "Smirnov Nikolay Pavlovich", "CE", "4505555555" },
                    { 6, 3, "Popov Sergey Vladimirovich", "C", "4506666666" },
                    { 7, 18, "Fedorov Andrey Olegovich", "CE", "4507777777" },
                    { 8, 7, "Morozov Pavel Igorevich", "C", "4508888888" },
                    { 9, 25, "Volkov Oleg Viktorovich", "CE", "4509999999" },
                    { 10, 10, "Orlov Maxim Yurievich", "C", "4510101010" }
                });

            migrationBuilder.InsertData(
                table: "vehicle_models",
                columns: new[] { "id", "body_type", "body_volume" },
                values: new object[,]
                {
                    { 1, "Tent", 60.0 },
                    { 2, "Refrigerator", 40.0 },
                    { 3, "Tank", 30.0 },
                    { 4, "Flatbed", 80.0 },
                    { 5, "Container", 70.0 },
                    { 6, "Isothermal", 50.0 },
                    { 7, "Tent", 90.0 },
                    { 8, "Refrigerator", 35.0 },
                    { 9, "Container", 100.0 },
                    { 10, "Flatbed", 75.0 }
                });

            migrationBuilder.InsertData(
                table: "vehicles",
                columns: new[] { "id", "brand", "license_plate", "load_capacity", "vehicle_model_id" },
                values: new object[,]
                {
                    { 1, "Volvo", "AA1234A", 20.0, 1 },
                    { 2, "Scania", "BB2345B", 25.0, 2 },
                    { 3, "MAN", "CC3456C", 15.0, 3 },
                    { 4, "KAMAZ", "DD4567D", 30.0, 4 },
                    { 5, "GAZ", "EE5678E", 10.0, 5 },
                    { 6, "DAF", "FF6789F", 22.0, 6 },
                    { 7, "Volvo", "GG7890G", 18.0, 7 },
                    { 8, "Scania", "HH8901H", 27.0, 8 },
                    { 9, "MAN", "II9012I", 35.0, 9 },
                    { 10, "KAMAZ", "JJ0123J", 12.0, 10 }
                });

            migrationBuilder.InsertData(
                table: "trips",
                columns: new[] { "id", "cargo_weight", "client_id", "cost", "departure_date", "destination", "driver_id", "origin", "vehicle_id" },
                values: new object[,]
                {
                    { 1, 15.0, 1, 80000m, new DateOnly(2026, 1, 10), "SPb", 1, "Moscow", 1 },
                    { 2, 12.0, 3, 60000m, new DateOnly(2026, 2, 5), "Kazan", 1, "Moscow", 1 },
                    { 3, 18.0, 5, 90000m, new DateOnly(2026, 3, 12), "Novgorod", 1, "SPb", 2 },
                    { 4, 10.0, 2, 50000m, new DateOnly(2026, 1, 20), "Moscow", 2, "Kazan", 3 },
                    { 5, 25.0, 4, 120000m, new DateOnly(2026, 4, 1), "Sochi", 2, "Moscow", 4 },
                    { 6, 8.0, 1, 45000m, new DateOnly(2026, 1, 15), "Rostov", 3, "Moscow", 5 },
                    { 7, 20.0, 7, 100000m, new DateOnly(2026, 2, 20), "Moscow", 3, "Rostov", 6 },
                    { 8, 14.0, 9, 70000m, new DateOnly(2026, 3, 5), "Tver", 3, "Moscow", 1 },
                    { 9, 16.0, 1, 85000m, new DateOnly(2026, 4, 10), "Moscow", 3, "Tver", 7 },
                    { 10, 22.0, 6, 110000m, new DateOnly(2026, 2, 1), "Omsk", 4, "Moscow", 8 },
                    { 11, 30.0, 10, 150000m, new DateOnly(2026, 1, 25), "Moscow", 5, "Omsk", 9 },
                    { 12, 28.0, 8, 140000m, new DateOnly(2026, 3, 18), "Ufa", 5, "Moscow", 9 },
                    { 13, 11.0, 3, 55000m, new DateOnly(2026, 4, 22), "Moscow", 5, "Ufa", 10 },
                    { 14, 9.0, 2, 48000m, new DateOnly(2026, 2, 14), "Kursk", 6, "Moscow", 2 },
                    { 15, 13.0, 5, 65000m, new DateOnly(2026, 3, 1), "Moscow", 7, "Kursk", 3 },
                    { 16, 17.0, 1, 90000m, new DateOnly(2026, 4, 5), "Voronezh", 7, "Moscow", 4 },
                    { 17, 7.0, 7, 40000m, new DateOnly(2026, 1, 30), "Moscow", 8, "Voronezh", 5 },
                    { 18, 21.0, 4, 105000m, new DateOnly(2026, 2, 25), "Murmansk", 9, "Moscow", 6 },
                    { 19, 17.0, 9, 98000m, new DateOnly(2026, 4, 15), "Moscow", 9, "Murmansk", 7 },
                    { 20, 24.0, 6, 115000m, new DateOnly(2026, 3, 22), "Yaroslavl", 10, "Moscow", 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_drivers_license_number",
                table: "drivers",
                column: "license_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trips_client_id",
                table: "trips",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_trips_driver_id",
                table: "trips",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "IX_trips_vehicle_id",
                table: "trips",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_license_plate",
                table: "vehicles",
                column: "license_plate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_vehicle_model_id",
                table: "vehicles",
                column: "vehicle_model_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "trips");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "drivers");

            migrationBuilder.DropTable(
                name: "vehicles");

            migrationBuilder.DropTable(
                name: "vehicle_models");
        }
    }
}
