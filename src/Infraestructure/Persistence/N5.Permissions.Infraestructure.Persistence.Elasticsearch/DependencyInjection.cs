using System.Diagnostics.CodeAnalysis;
using Elastic.Clients.Elasticsearch;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N5.Permissions.Domain.Interfaces.Infraestructure.Services;
using N5.Permissions.Infraestructure.Persistence.Elasticsearch.Configuration;
using N5.Permissions.Infraestructure.Persistence.Elasticsearch.Services;

namespace N5.Permissions.Infraestructure.Persistence.Elasticsearch;

/// <summary>
/// Dependency injection configuration for Elasticsearch.
/// </summary>
[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    /// <summary>
    /// Adds the Elasticsearch services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The application configuration.</param>
    public static IServiceCollection AddElasticSearch(this IServiceCollection services, IConfiguration configuration)
    {
        var setting = configuration.GetSection("ElasticSearch").Get<ElasticSearchSetting>();

        services.AddSingleton((sp) =>
        {
            var settings = new ElasticsearchClientSettings(new Uri(setting?.Url ?? string.Empty))
                .DefaultIndex(setting?.Index ?? string.Empty);

            return new ElasticsearchClient(settings);
        });

        services.AddSingleton<IIndexationService, ElasticSearchService>();
        return services;
    }
}