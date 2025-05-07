using FluentResults;
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

        [JsonProperty("order_date", Required = Required.Default)]
        public DateTime? OrderDate { get; set; }

        [JsonProperty("article_positions", Required = Required.Always)]
        public List<CustomerOrderArticlePosition> ArticlePositions { get; set; }


        [JsonProperty("packing_configuration", Required = Required.Default)]
        public PackingConfiguration PackingConfiguration { get; set; }

        public Result isValid()
        {
            var onlyNumericOrderId = bool.Parse(Environment.GetEnvironmentVariable("NUMERIC_ORDER_ID") ?? "false");

            if (string.IsNullOrEmpty(OrderId)) { return Result.Fail("Invalid OrderId"); }
            if (onlyNumericOrderId && !long.TryParse(OrderId, out _)) { return Result.Fail($"Invalid OrderId: {OrderId}"); }

            if (!Enum.IsDefined(typeof(OrderType), OrderType)) { return Result.Fail("Invalid OrderType"); }

            if (Priority < 0 || Priority > 100) { return Result.Fail("Invalid Priority"); }

            if (ArticlePositions == null || ArticlePositions.Count == 0) {
                return Result.Fail("Order has no ArticlePositions");
            }

            var automatedArticles = ArticlePositions.Where(a => a.PackingStrategy == ArticlePackingStrategy.Automatic);
            if(automatedArticles.Count() == 0) {
                return Result.Fail("Order has no automated articles");
            }

            var articleValidation = Result.Ok();
            foreach (CustomerOrderArticlePosition article in ArticlePositions)
            {
                var res = article.isValid();
                articleValidation = Result.Merge(articleValidation, res);
            }
            if (articleValidation.IsFailed)
            {
                var articleErrors = string.Join("\n  ", articleValidation.Errors.Select(e => e.Message));
                return Result.Fail($"Order has invalid articles:\n  {articleErrors}");
            }

            return Result.Ok();
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

        public Result isValid()
        {
            if (string.IsNullOrEmpty(ArticleId)) 
            {
                return Result.Fail($"Invalid ArticleId: {ArticleId}");
            }

            if (!Enum.IsDefined(typeof(ArticlePackingStrategy), PackingStrategy)) 
            {
                return Result.Fail($"Invalid PackingStrategy for article {ArticleId}");
            }

            if(MAX_HEIGHT == 0) { MAX_HEIGHT = int.MaxValue; }
            if(MAX_WIDTH == 0) { MAX_WIDTH = int.MaxValue; }
            if(MAX_LENGTH == 0) { MAX_LENGTH = int.MaxValue; }

            // only check dimensions if packing strategy is automatic
            if (PackingStrategy == ArticlePackingStrategy.Automatic)
            {
                if (Width <= 0 || Width >= MAX_WIDTH) { return Result.Fail($"Invalid Width for article {ArticleId}: {Width}"); }
                if (Height <= 0 || Height >= MAX_HEIGHT) { return Result.Fail($"Invalid Height for article {ArticleId}: {Height}");  }
                if (Length <= 0 || Length >= MAX_LENGTH) { return Result.Fail($"Invalid Length for article {ArticleId}: {Length}"); }

                if (Weight < 0) { return Result.Fail($"Weight is less than 0 for article {ArticleId}"); }
                if (Amount <= 0) { return Result.Fail($"Amount is less or equal to 0 for article {ArticleId}"); }
            }

            return Result.Ok();
        }
    }
}
