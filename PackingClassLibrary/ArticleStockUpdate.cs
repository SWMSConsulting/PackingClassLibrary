using Newtonsoft.Json;

namespace PackingClassLibrary
{
    public class ArticleStockUpdate: Serializable
    {
        [JsonProperty("articleNumber", Required = Required.Always)]
        public string ArticleNumber { get; set; }

        [JsonProperty("availableStock", Required = Required.Always)]
        public int AvailableStock { get; set; }
    }
}
