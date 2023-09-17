

namespace BuildingBlocks.Core.Models
{
    public class Card
    {
        public int Id { get; init; }
        public int FbId { get; init; }
        public int FbDataId { get; init; }
        public string Name { get; set; } = string.Empty;
        public Pc PcPrices { get; set; }
        public Ps psPrices {get;set;}

    }
}