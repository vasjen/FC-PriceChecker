using System.Text;
using BuildingBlocks.Core.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace PriceChecker
{
    public class Processor
    {
        private readonly HttpClient _httpClient;

        public Processor(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
        
        public async Task<Card> CallScraperToGetCard(int id)
        {
            
            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5278/{id}");
            response.EnsureSuccessStatusCode();

            // Получите карту из ответа Scraper
            var cardJson = await response.Content.ReadAsStringAsync();
            var card = JsonConvert.DeserializeObject<Card>(cardJson);
            System.Console.WriteLine("Card: {0}", card);
            return card;
        }
        public async Task SendMessageToCardController(Card card)
        {
            // Создайте подключение к RabbitMQ
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "cards", type: ExchangeType.Fanout);
                
                var json = JsonConvert.SerializeObject(card);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: "cards",
                     routingKey: string.Empty,
                     basicProperties: null,
                     body: body);
            }
        }
    }
}