

using System.Text;
using System.Text.Json;

namespace Scraper.Services
{
    public class InitialService : IInitialService
    {
        private readonly HttpClient _httpClient;
        private readonly IScraperService _cardService;
        private readonly IRequestWithProxy _requestWithProxy;

        public InitialService(
            IHttpClientFactory httpClientFactory, 
            IScraperService cardService, 
            IRequestWithProxy requestWithProxy)
        {
            _httpClient = httpClientFactory.CreateClient("fb");
            _cardService = cardService;
            _requestWithProxy = requestWithProxy;
        }
        public async Task Init()
        {
            //1. Get MaxId from local DB
            //2. Get MaxId from Futbin
            //3. If MaxId from Futbin > MaxId from local DB
            //   for (int i = MaxId from local DB; i < MaxId from Futbin; i++)
            //      GetCard(i)
            //4. SaveCardToDb (call POST /api/cards)
            var client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5079/Card");
            var MaxId = int.Parse(await response.Content.ReadAsStringAsync());
            var MaxIdFb = await _cardService.GetMaxId();
            System.Console.WriteLine(MaxIdFb);
            if (MaxIdFb > MaxId)
            {
                Parallel.ForEach(InitIdRange(MaxId + 1, MaxIdFb), new ParallelOptions { MaxDegreeOfParallelism = 40 },  id =>  GetCard(id).Wait());
                // for (int i = MaxId + 1; i <= MaxIdFb; i++)
                // {
// 
                    // await GetCard(i);
                //    
                // }
            }
        }
        private IEnumerable<int> InitIdRange(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                yield return i;
            }
        }
        private async Task GetCard(int id)
        {
            Console.WriteLine("Getting card with id: {0}", id);
                    var card = await _cardService.GetCard(id);
                    if (card is null)
                    {
                        Console.WriteLine("With id: {0} card is null",id);    
                    }
                    else
                    {
                        var json = JsonSerializer.Serialize(card);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var req = await _httpClient.PostAsync("http://localhost:5079/Card", content);
                        // Console.WriteLine(req.StatusCode);
                        // Console.WriteLine(await req.Content.ReadAsStringAsync());
                    }
        }
    }
}