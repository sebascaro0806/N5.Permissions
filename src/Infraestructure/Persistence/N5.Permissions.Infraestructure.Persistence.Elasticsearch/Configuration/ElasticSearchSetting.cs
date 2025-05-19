namespace N5.Permissions.Infraestructure.Persistence.Elasticsearch.Configuration;

/// <summary>
/// Configuration settings for the ElasticSearch server.
/// </summary>
public class ElasticSearchSetting
{
    /// <summary>
    /// The URL of the ElasticSearch server.
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// The index name for the ElasticSearch server.
    /// </summary>
    public string Index { get; set; } = string.Empty;
}
