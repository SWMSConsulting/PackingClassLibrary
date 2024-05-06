using Newtonsoft.Json;

namespace PackingClassLibrary
{
    public class LabelRequest : Serializable
    {
        [JsonProperty("order_id", Required = Required.Always)]
        public string OrderId { get; set; }


        [JsonProperty("amout_pallets", Required = Required.Always)]
        public int AmountPallets { get; set; }
    }
}
