using Newtonsoft.Json;

namespace PackingClassLibrary
{
    public class AgentControl : Serializable
    {
        [JsonProperty("updated_timestamp", Required = Required.Default)]
        public string? UpdatedTimestamp { get; set; }

        [JsonProperty("block_manual_packing", Required = Required.Default)]
        public bool? BlockManualPacking { get; set; }
    }
}
