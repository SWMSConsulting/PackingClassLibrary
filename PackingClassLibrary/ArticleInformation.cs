using Newtonsoft.Json;

namespace PackingClassLibrary
{
    public class ArticleInformation : Serializable
    {
        [JsonProperty("article_number", Required = Required.Always)]
        public string ArticleNumber { get; set; }

        [JsonProperty("description", Required = Required.Default)]
        public string? Description { get; set; }

        [JsonProperty("automation_type", Required = Required.Always)]
        public string AutomationType { get; set; }

        [JsonProperty("length", Required = Required.Always)]
        public int Length { get; set; }

        [JsonProperty("width", Required = Required.Always)]
        public int Width { get; set; }

        [JsonProperty("height", Required = Required.Always)]
        public int Height { get; set; }

        [JsonProperty("weight", Required = Required.Default)]
        public double? Weight { get; set; }
    }
}
