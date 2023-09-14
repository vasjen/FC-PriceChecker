using System.Text.Json;
using System.Text.Json.Nodes;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Scraper.Models;

namespace Scraper.Services
{
    public class ScraperService : IScraperService
    {
        private readonly HttpClient _httpClient;

        public ScraperService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("fb");

        }
        

        

        public async Task<string?> GetCard(string id)
        {
            var page = await _httpClient.GetStringAsync($"24/player/{id}");
            // System.Console.WriteLine(page);
            var doc = new HtmlDocument();
            doc.LoadHtml(page);
            var name = doc.DocumentNode.SelectSingleNode("(//td[@class='table-row-text'])[1]").InnerText;
            var FbDataId = doc.DocumentNode.SelectSingleNode("(//td[@class='table-row-text'])[15]").InnerText;
            var priceResponse = await _httpClient.GetStringAsync($"24/playerPrices?player={FbDataId}");
            JsonNode jsonNod = JsonNode.Parse(priceResponse);
            var Price = jsonNod[$"{FbDataId}"]!["prices"]["ps"]!["LCPrice"].GetValue<int>();
            var result = $"Name: {name} with ID: {FbDataId} and price: {Price} \nPrice Response: {priceResponse}";
            
            var prices = JsonSerializer.Deserialize<PricesJson>(priceResponse);
            System.Console.WriteLine(prices?.Id);
            
            // ans: {"255565":{"prices":{"ps":{"LCPrice":0,"LCPrice2":0,"LCPrice3":0,"LCPrice4":0,"LCPrice5":0,"updated":0,"MinPrice":0,"MaxPrice":0,"PRP":0,"LCPClosing":0},"pc":{"LCPrice":0,"LCPrice2":0,"LCPrice3":0,"LCPrice4":0,"LCPrice5":0,"updated":"Never","MinPrice":0,"MaxPrice":0,"PRP":0,"LCPClosing":0}}}}


            return result;
            
        }
    }
}