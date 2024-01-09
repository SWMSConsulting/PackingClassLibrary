using Newtonsoft.Json;

namespace PackingClassLibrary
{
    public class PalletWeight
    {
        [JsonProperty("order_id", Required = Required.Always)]
        public string OrderId { get; set; }

        [JsonProperty("pallet_index", Required = Required.Always)]
        public int PalletIndex { get; set; }

        [JsonProperty("weight", Required = Required.Always)]
        public double WeightValue { get; set; }
    }
}
