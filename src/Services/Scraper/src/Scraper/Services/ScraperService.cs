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
        HtmlDocument PlayerPage { get; set; }
        
        public async Task<Card?> GetCard(int id)
        {
            PlayerPage = await GetHtmlDocument(link+id.ToString());
            if (PlayerPage is null)
                return null;
            
            var name = ParseFromDoc("(//td[@class='table-row-text'])[1]");
            var FbDataId = ParseFromDoc("//th[text()='ID']/following-sibling::td");
            var price = await GetPriceAsync(int.Parse(FbDataId));
            var psPrices = GetPsPrices(price);
            var pcPrices = GetPcPrices(price);
            var revision = ParseFromDoc("//th[text()='Revision']/following-sibling::td");
            var raiting = ParseFromDoc("//*[@id=\"Player-card\"]/div[2]");
            var position = ParseFromDoc("//*[@id=\"Player-card\"]/div[4]");
            var displayedName = ParseFromDoc("//*[@id=\"Player-card\"]/div[3]");
            
            return new Card
            {
                Id = id,
                FbId = id,
                FbDataId = int.Parse(FbDataId),
                Name = name,
                DisplayedName = displayedName,
                PcPrices = pcPrices,
                PsPrices = psPrices,
                Position = position,
                Revision = revision,
                Raiting = int.Parse(raiting)
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

        private async Task<string> GetPriceAsync(int FbDataId)
            => await _httpClient.GetStringAsync($"24/playerPrices?player={FbDataId}");

        public string ParseFromDoc(string xPath)
            => PlayerPage.DocumentNode
                .SelectSingleNode(xPath)
                .InnerText;
    }
}