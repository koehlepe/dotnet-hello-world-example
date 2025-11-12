var builder = DistributedApplication.CreateBuilder(args);

var keycloak = builder.AddKeycloak("keycloak", 8080);

var apiService = builder.AddProject<Projects.MyAspireApp_ApiService>("apiservice")
    .WithReference(keycloak)
    .WaitFor(keycloak)
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.MyAspireApp_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithHttpHealthCheck("/health")
    .WithReference(keycloak)
    .WaitFor(keycloak)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
