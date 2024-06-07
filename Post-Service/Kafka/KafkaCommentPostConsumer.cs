using Confluent.Kafka;

namespace Post_Service.Kafka;

public class KafkaCommentPostConsumer : IHostedService
{
    private readonly ILogger<KafkaCommentPostConsumer> _logger;
    private readonly IConfiguration _configuration;
    private IConsumer<Ignore, string> _consumer;

    public KafkaCommentPostConsumer(ILogger<KafkaCommentPostConsumer> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        _logger.LogInformation("Initializing consumer...");
        
        var bootstrapServers = _configuration.GetSection("Kafka:BootstrapServers").Value;
        var groupId = _configuration.GetSection("Kafka:GroupId").Value;
        
        var config = new ConsumerConfig()
        {
            BootstrapServers = bootstrapServers,
            GroupId = groupId,
            AllowAutoCreateTopics = true,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        _logger.LogInformation("Initialized consumer...");
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _consumer.Subscribe("test-topic");
        _logger.LogInformation("Subscribed to test-topic...");

        Task.Run(() =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(cancellationToken);

                    var message = consumeResult.Message.Value;

                    _logger.LogInformation($"Received message: {message}");
                }
                catch (OperationCanceledException ex)
                {
                    _logger.LogError($"Operation was cancelled: {ex.Message}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error processing Kafka message: {ex.Message}");
                }
            }
        });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Closing consumer...");
        _consumer.Close();
        return Task.CompletedTask;
    }
}