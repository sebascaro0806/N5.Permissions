using System.Text.Json;
using Confluent.Kafka;
using Moq;
using N5.Permissions.Infraestructure.Messaging.Kafka.Events;

namespace N5.Permissions.Services.Test.Infraestructure.Events;

[TestFixture]
public class KafkaProducerTest
{
    [Test]
    public async Task PublishAsync_ShouldCallProduceAsync_WithSerializedMessage()
    {
        // Arrange
        var mockProducer = new Mock<IProducer<Null, string>>();
        var testMessage = new { Id = 1, Name = "Test Message" };
        var topic = "test-topic";
        var expectedJson = JsonSerializer.Serialize(testMessage);

        mockProducer
            .Setup(p => p.ProduceAsync(
                topic,
                It.Is<Message<Null, string>>(m => m.Value == expectedJson),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new DeliveryResult<Null, string>());

        var kafkaProducer = new KafkaProducer(mockProducer.Object);

        // Act
        await kafkaProducer.PublishAsync(testMessage, topic);

        // Assert
        mockProducer.Verify(p => p.ProduceAsync(
            topic,
            It.Is<Message<Null, string>>(m => m.Value == expectedJson),
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
