using Newtonsoft.Json;

namespace PackingClassLibrary
{
    public class AutomationType : Serializable
    {
        [JsonProperty("article_id", Required = Required.Always)]
        public string ArticleId { get; set; }

        [JsonProperty("material_name", Required = Required.Always)]
        public string MaterialName { get; set; }

        [JsonProperty("material_number", Required = Required.Always)]
        public long MaterialNumber { get; set; }

        [JsonProperty("length", Required = Required.Always)]
        public int Length { get; set; } // 0-65535

        [JsonProperty("width", Required = Required.Always)]
        public int Width { get; set; } // 0-65535

        [JsonProperty("thickness", Required = Required.Always)]
        public int Thickness { get; set; } // 0-255

        [JsonProperty("group", Required = Required.Always)]
        public int Group { get; set; } // // 0-9

        [JsonProperty("density", Required = Required.Always)]
        public int Density { get; set; } // 0-65535

        [JsonProperty("minimum_stock", Required = Required.Always)]
        public int MinimumStock { get; set; } // 0-65535

        [JsonProperty("etage", Required = Required.Always)]
        public int Etage { get; set; } // 0-255 (see documentation)

        [JsonProperty("customer_id", Required = Required.Always)]
        public string CustomerId { get; set; } // 14 sign
    }
}
