using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PackingClassLibrary
{
    public enum OrderType
    {
        Shipping,
        Pickup
    }
    public enum ArticlePackingStategy
    {
        Automatic,
        Manual,
    }

    public class CustomerOrder : Serializable
    {
        [JsonProperty("order_id", Required = Required.Always)]
        public string OrderId { get; set; }

        
        [JsonProperty("order_type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType OrderType { get; set; }


        [JsonProperty("priority", Required = Required.Always)]
        public int Priority { get; set; }


        [JsonProperty("article_positions", Required = Required.Always)]
        public List<CustomerOrderArticlePosition> ArticlePositions { get; set; }

        public bool isValid()
        {
            if (string.IsNullOrEmpty(OrderId)) { return false; }
            if (!long.TryParse(OrderId, out _)) { return false; }

            if (!Enum.IsDefined(typeof(OrderType), OrderType)) { return false; }

            if (Priority < 0 || Priority > 100) { return false; }

            if (ArticlePositions == null || ArticlePositions.Count == 0) { return false; }

            foreach (CustomerOrderArticlePosition article in ArticlePositions)
            {
                if (!article.isValid()) { return false; }
            }

            return true;
        }
    }

    public class CustomerOrderArticlePosition
    {
        public const int MAX_WIDTH = 800;
        public const int MAX_HEIGHT = 2000;
        public const int MAX_LENGTH = 9999;

        [JsonProperty("article_id", Required = Required.Always)]
        public string ArticleId { get; set; }


        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; }


        [JsonProperty("width", Required = Required.Always)]
        public double Width { get; set; }

        [JsonProperty("height", Required = Required.Always)]
        public double Height { get; set; }

        [JsonProperty("length", Required = Required.Always)]
        public double Length { get; set; }


        [JsonProperty("weight", Required = Required.Always)]
        public double Weight { get; set; }


        [JsonProperty("amount", Required = Required.Always)]
        public int Amount { get; set; }


        [JsonProperty("packing_strategy", Required = Required.Always)]
        public int PackingStrategy { get; set; }

        public bool isValid()
        {
            if (string.IsNullOrEmpty(ArticleId)) { return false; }

            if (Width <= 0 || Width >= MAX_WIDTH) { return false; }
            if (Height <= 0 || Height >= MAX_HEIGHT) { return false; }
            if (Length <= 0 || Length >= MAX_LENGTH) { return false; }

            if (Weight < 0) { return false; }
            if (Amount <= 0) { return false; }

            if (!Enum.IsDefined(typeof(ArticlePackingStategy), PackingStrategy)) { return false; }

            return true;
        }
    }
}
