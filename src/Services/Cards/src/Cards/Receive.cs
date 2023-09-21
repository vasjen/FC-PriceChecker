using System.Text;
using BuildingBlocks.Core.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace Cards;
public class Receive

{
    private readonly ILogger<Receive> _logger;

    public Receive(ILogger<Receive> logger)
    {
        _logger = logger;
    }
    // В CardController

    public async Task ConsumeCardMessage(CancellationToken cancellationToken)
    {
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: "cards", type: ExchangeType.Fanout);
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName,
                              exchange: "cards",
                              routingKey: string.Empty);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var card = JsonConvert.DeserializeObject<Card>(message); 
                _logger.LogWarning($"Received {card.Name}");
            };
            channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);
                await Task.Delay(1000, cancellationToken); // подождите 1 секунду, прежде чем проверить отмену
            }
        }
        catch (OperationCanceledException ex)
        {
            _logger.LogInformation("Operation was cancelled: {0}", ex.Message);
        }
        
    }


}