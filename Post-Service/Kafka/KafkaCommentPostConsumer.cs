using Confluent.Kafka;
using Post_Service.Services;

namespace Post_Service.Kafka;

public class KafkaCommentPostConsumer : IHostedService
{
    private readonly ILogger<KafkaCommentPostConsumer> _logger;
    private readonly IConfiguration _configuration;
    private readonly IPostService _postService;
    private IConsumer<Ignore, string> _consumer;

    public KafkaCommentPostConsumer(ILogger<KafkaCommentPostConsumer> logger, IConfiguration configuration, IPostService postService)
    {
        _logger = logger;
        _configuration = configuration;
        _postService = postService;
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
        _consumer.Subscribe("comment-created");
        _logger.LogInformation("Subscribed to comment-created...");

        Task.Run(() =>
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(cancellationToken);

                    var message = consumeResult.Message.Value;

                    _logger.LogInformation("Received message...");
                    _logger.LogInformation($"Updating CommentCount of post: {message}");
                    
                    _postService.IncrementCommentCount(message).Wait();
                    _logger.LogInformation($"Incremented CommentCount of post: {message}");
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