namespace Scraper.Models
{
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

    [JsonSerializable(typeof(Dictionary<string, Prices>))]
    public class PricesJson
    {
        public string Id { get; set; }
        [JsonPropertyName("prices")]
        public Prices Prices { get; set; }
    }

    [JsonSerializable(typeof(Prices))]
    public class Prices
    {
        public PS PS { get; set; }
        public PC PC { get; set; }
    }
    [JsonSerializable(typeof(PS))]
    public class PS : PriceBase
    {
       
    }
    [JsonSerializable(typeof(PC))]
    public class PC : PriceBase
    {
        public new string updated { get; set; }    
    }
    public class PriceBase
    {
        public int LCPrice { get; set; }
        public int LCPrice2 { get; set; }
        public int LCPrice3 { get; set; }
        public int LCPrice4 { get; set; }
        public int LCPrice5 { get; set; }
        public int updated { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public int PRP { get; set; }
        public int LCPClosing { get; set; }
    }
}