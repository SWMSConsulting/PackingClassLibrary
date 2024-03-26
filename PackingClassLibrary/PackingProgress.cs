using Newtonsoft.Json;

namespace PackingClassLibrary
{

    public class PackingProgress : Serializable
    {
        [JsonProperty("order_id", Required = Required.Always)]
        public string OrderId { get; set; }

        [JsonProperty("pallet_index", Required = Required.Always)]
        public int PalletIndex { get; set; }

        [JsonProperty("packed_articles", Required = Required.Always)]
        public List<PackingProgressArticle> PackedArticles { get; set; }
    }

    public class PackingProgressArticle : Serializable
    {
        [JsonProperty("article_identifier", Required = Required.Always)]
        public string ArticleIdentifier { get; set; }

        [JsonProperty("quantity", Required = Required.Always)]
        public int Quantity { get; set; }
    }
}
