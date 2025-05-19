using Elastic.Clients.Elasticsearch;
using Moq;
using N5.Permissions.Infraestructure.Persistence.Elasticsearch.Services;

namespace N5.Permissions.Services.Test.Infraestructure.Services;

/// <summary>
/// Test class for the ElasticSearchService.
/// </summary>
[TestFixture]
public class ElasticSearchServiceTest
{
    /// <summary>
    /// Test for the IndexDocumentAsync method of the ElasticSearchService.
    /// </summary>
    [Test]
    public async Task IndexDocumentAsync_CallsClientIndexAsync()
    {
        // Arrange
        var document = new { Id = 1, Name = "Test Document" };
        var mockClient = new Mock<ElasticsearchClient>();

        mockClient
            .Setup(c => c.IndexAsync(
                document,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new IndexResponse());

        var service = new ElasticSearchService(mockClient.Object);

        // Act
        await service.IndexDocumentAsync(document);

        // Assert
        mockClient.Verify(c => c.IndexAsync(
            document,
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
