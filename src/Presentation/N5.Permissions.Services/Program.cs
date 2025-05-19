
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Exceptions;
using N5.Permissions.Application;
using N5.Permissions.Infraestructure.Messaging.Kafka;
using N5.Permissions.Infraestructure.Persistence.Elasticsearch;
using N5.Permissions.Services.Handlers;

try
{
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("ApplicationContext", "N5.Permissions.Services")
        .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
        .Enrich.WithExceptionDetails()
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    builder.Services
        .AddOptions()
        .AddApplication()
        .AddTransient<ElasticsearchIndexerHandler>()
        .AddElasticSearch(builder.Configuration)
        .AddKafkaMessaging(builder.Configuration);

    var app = builder.Build();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal($"Program terminated unexpectedly - {ex.Message}");
}
finally
{
    Log.CloseAndFlush();
}