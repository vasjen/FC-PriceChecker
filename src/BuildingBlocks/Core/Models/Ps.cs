using System.Text.Json.Serialization;

namespace BuildingBlocks.Core.Models
{
    public class Ps
    {
        [JsonIgnore]
        public int Id { get; init; }
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