
var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume();

var database = postgres.AddDatabase("TransportCompanyDb");

builder.AddProject<Projects.TransportCompany_Api_Host>("api")
    .WithReference(database)
    .WaitFor(database);

builder.Build().Run();
