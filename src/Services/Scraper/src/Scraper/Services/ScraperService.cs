using BuildingBlocks.Core.Models;
using HtmlAgilityPack;
using Newtonsoft.Json;


namespace Scraper.Services
{
    public class ScraperService : IScraperService
    {
        private readonly HttpClient _httpClient;
        private const string link = "24/player/";
        private const string latestPlayersLink = "https://www.futbin.com/latest";

        public ScraperService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("fb");

        }
        
        public async Task<Card?> GetCard(int id)
        {
            var doc = await GetHtmlDocument(link+id.ToString());
            if (doc is null)
                return null;
            
            var name = GetPlayerName(doc);
            var FbDataId = GetFbDataId(doc);
            var price = await GetPriceAsync(FbDataId);
            var psPrices = GetPsPrices(price);
            var pcPrices = GetPcPrices(price);

         
            return new Card
            {
                Id = id,
                FbId = id,
                FbDataId = int.Parse(FbDataId),
                Name = name,
                PcPrices = pcPrices,
                psPrices = psPrices
            };
            
        }

        public async Task<int> GetMaxId()
        {
            var doc = await GetHtmlDocument(latestPlayersLink);
            if (doc is null)
                return 0;

            HtmlNode node = doc.DocumentNode.SelectSingleNode("//div[@class=' get-tp']");
            string dataSiteId = node?.Attributes["data-site-id"]?.Value;

            if (dataSiteId != null)
                return int.Parse(dataSiteId);
            
            else
                return 0;
        }

        private async Task<HtmlDocument?> GetHtmlDocument(string link)
        {
            var page = await _httpClient.GetStringAsync(link);
            var doc = new HtmlDocument();
            doc.LoadHtml(page);
            return doc;
        }
        private Ps GetPsPrices(string priceResponse)
            =>  JsonConvert.DeserializeObject<Ps>(priceResponse);

        private Pc GetPcPrices(string priceResponse)
            =>  JsonConvert.DeserializeObject<Pc>(priceResponse);
        private string GetPlayerName(HtmlDocument doc)
            => doc.DocumentNode
                  .SelectSingleNode("(//td[@class='table-row-text'])[1]")
                  .InnerText;
            
        private string GetFbDataId(HtmlDocument doc)
            => doc.DocumentNode
                  .SelectSingleNode("//th[text()='ID']/following-sibling::td")
                  .InnerText.Trim();
           
        private async Task<string> GetPriceAsync(string FbDataId)
            => await _httpClient.GetStringAsync($"24/playerPrices?player={FbDataId}");

        
    }
}