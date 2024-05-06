using Newtonsoft.Json;

namespace PackingClassLibrary
{
    public class ArticleInformation : Serializable
    {
        [JsonProperty("article_number", Required = Required.Always)]
        public int ArticleNumber { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("length", Required = Required.Always)]
        public double Length { get; set; }

        [JsonProperty("width", Required = Required.Always)]
        public double Width { get; set; }

        [JsonProperty("height", Required = Required.Always)]
        public double Height { get; set; }

        [JsonProperty("weight", Required = Required.Default)]
        public double? Weight { get; set; }

        [JsonProperty("minimum_stock", Required = Required.Default)]
        public int? MinimumStock { get; set; }
    }
}
