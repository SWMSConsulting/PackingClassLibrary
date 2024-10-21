using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PackingClassLibrary
{
    public enum OrderType
    {
        Shipping,
        PickUp
    }
    public enum ArticlePackingStrategy
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

        [JsonProperty("packing_configuration", Required = Required.Default)]
        public CustomerOrderPackingConfiguration? PackingConfiguration { get; set; } = null;

        [JsonProperty("article_positions", Required = Required.Always)]
        public List<CustomerOrderArticlePosition> ArticlePositions { get; set; }

        public bool isValid()
        {
            // TODO: change to result
            if (string.IsNullOrEmpty(OrderId)) { return false; }
            //if (!long.TryParse(OrderId, out _)) { return false; }

            if (!Enum.IsDefined(typeof(OrderType), OrderType)) { return false; }

            if (Priority < 0 || Priority > 100) { return false; }

            if (ArticlePositions == null || ArticlePositions.Count == 0) { return false; }

            var automatedArticles = ArticlePositions.Where(a => a.PackingStrategy == ArticlePackingStrategy.Automatic);
            if(automatedArticles.Count() == 0) { return false; }    

            foreach (CustomerOrderArticlePosition article in ArticlePositions)
            {
                if (!article.isValid()) { 
                    Console.WriteLine("Article is not valid: " + article.ArticleId);
                    return false; 
                }
            }

            return true;
        }
    }

    public class CustomerOrderArticlePosition
    {
        public static int MAX_WIDTH = Convert.ToInt32(Environment.GetEnvironmentVariable("MAX_WIDTH") ?? "0");
        public static int MAX_HEIGHT = Convert.ToInt32(Environment.GetEnvironmentVariable("MAX_HEIGHT") ?? "0");
        public static int MAX_LENGTH = Convert.ToInt32(Environment.GetEnvironmentVariable("MAX_LENGTH") ?? "0");

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
        public ArticlePackingStrategy PackingStrategy { get; set; } = ArticlePackingStrategy.Manual;

        public bool isValid()
        {
            if (string.IsNullOrEmpty(ArticleId)) { return false; }

            if (!Enum.IsDefined(typeof(ArticlePackingStrategy), PackingStrategy)) { return false; }

            if(MAX_HEIGHT == 0) { MAX_HEIGHT = int.MaxValue; }
            if(MAX_WIDTH == 0) { MAX_WIDTH = int.MaxValue; }
            if(MAX_LENGTH == 0) { MAX_LENGTH = int.MaxValue; }

            // only check dimensions if packing strategy is automatic
            if (PackingStrategy == ArticlePackingStrategy.Automatic)
            {
                if (Width <= 0 || Width >= MAX_WIDTH) { return false; }
                if (Height <= 0 || Height >= MAX_HEIGHT) { return false; }
                if (Length <= 0 || Length >= MAX_LENGTH) { return false; }

                if (Weight < 0) { return false; }
                if (Amount <= 0) { return false; }
            }

            return true;
        }
    }
    public class CustomerOrderPackingConfiguration
    {
        [JsonProperty("max_width", Required = Required.Always)]
        public int MaxWidth { get; set; }

        [JsonProperty("max_height", Required = Required.Always)]
        public int MaxHeight { get; set; }

        [JsonProperty("max_length", Required = Required.Always)]
        public int MaxLength { get; set; }

        [JsonProperty("max_weight", Required = Required.Default)]
        public int? MaxWeight { get; set; } = null;


        [JsonProperty("pallet_width", Required = Required.Default)]
        public int? PalletWidth { get; set; } = null;

        [JsonProperty("pallet_height", Required = Required.Default)]
        public int? PalletHeight { get; set; } = null;

        [JsonProperty("pallet_length", Required = Required.Default)]
        public int? PalletLength { get; set; } = null;

        [JsonProperty("pallet_weight", Required = Required.Default)]
        public int? PalletWeight { get; set; } = null;


        [JsonProperty("alignment_length", Required = Required.Default)]
        public PackingAlignment AlignmentLength { get; set; } = PackingAlignment.Start;

        [JsonProperty("alignment_width", Required = Required.Default)]
        public PackingAlignment AlignmentWidth { get; set; } = PackingAlignment.Start;
    }

    public enum PackingAlignment
    {
        Center = 0,
        Start = 1,
        End = 2
    }
}
