using Newtonsoft.Json;

namespace PackingClassLibrary
{
    public class ArticleStockUpdate: Serializable
    {
        [JsonProperty("article_number", Required = Required.Always)]
        public string ArticleNumber { get; set; }

        [JsonProperty("available_stock", Required = Required.Always)]
        public int AvailableStock { get; set; }
    }
}
