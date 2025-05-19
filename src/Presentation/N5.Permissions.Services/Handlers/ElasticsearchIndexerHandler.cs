using Microsoft.Extensions.Logging;
using N5.Permissions.Domain;
using N5.Permissions.Domain.Dtos.Common;
using N5.Permissions.Domain.Interfaces.Infraestructure.Messaging;
using N5.Permissions.Domain.Interfaces.Infraestructure.Services;
using N5.Permissions.Infraestructure.Messaging.Kafka.Configuration;

namespace N5.Permissions.Services.Handlers;

/// <summary>
/// Handles events related to Elasticsearch indexing.
/// </summary>
[KafkaTopic(Topics.GetAllPermissions)]
[KafkaTopic(Topics.ModifyPermission)]
[KafkaTopic(Topics.RequestPermission)]
public class ElasticsearchIndexerHandler(
    IIndexationService _indexationService, ILogger<ElasticsearchIndexerHandler> _logger) : IEventConsumer<PermissionEventDto>
{
    /// <summary>
    /// Handles the event and performs indexing.
    /// </summary>
    /// <param name="event">The event to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public async Task HandleAsync(PermissionEventDto @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("ElasticsearchIndexerHandler: {Event}", @event.NameOperation);

        await _indexationService.IndexDocumentAsync(@event.Data);

        _logger.LogInformation("ElasticsearchIndexerHandler: {Event}", @event.NameOperation);
    }
}
