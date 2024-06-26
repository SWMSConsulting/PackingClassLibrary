﻿using Newtonsoft.Json;

namespace PackingClassLibrary
{
    public class ManualPackingOrder : Serializable
    {
        [JsonProperty("order_id", Required = Required.Always)]
        public string OrderId { get; set; }

        [JsonProperty("order_details", Required = Required.Default)]
        public string OrderDetails { get; set; }

        [JsonProperty("articles", Required = Required.Always)]
        public List<ManualPackingArticle> Articles { get; set; }
    }

    public class ManualPackingArticle : Serializable
    {
        [JsonProperty("article_id", Required = Required.Always)]
        public string ArticleId { get; set; }

        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; }

        [JsonProperty("amount", Required = Required.Always)]
        public int Amount { get; set; }

        [JsonProperty("amount_packed", Required = Required.Default)]
        public int AmountPacked { get; set; } = 0;
    }
}
