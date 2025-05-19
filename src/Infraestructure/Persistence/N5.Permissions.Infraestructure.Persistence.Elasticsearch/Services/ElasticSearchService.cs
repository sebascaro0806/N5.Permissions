using Elastic.Clients.Elasticsearch;
using N5.Permissions.Domain.Interfaces.Infraestructure.Services;

namespace N5.Permissions.Infraestructure.Persistence.Elasticsearch.Services;

/// <summary>
/// Service for indexing documents in Elasticsearch.
/// </summary>
public class ElasticSearchService(ElasticsearchClient _client) : IIndexationService
{
    /// <summary>
    /// Asynchronously indexes a document of type T.
    /// </summary>
    /// <typeparam name="T">The type of the document to index.</typeparam>
    /// <param name="document">The document to index.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task IndexDocumentAsync<T>(T document) where T : class
    {
        await _client.IndexAsync(document);
    }
}
