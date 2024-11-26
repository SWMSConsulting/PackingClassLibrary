using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;

namespace PackingClassLibrary
{
    public enum PalletPackingStrategy
    {
        Automatic,
        AutomaticWithManual,
    }

    public class AutomationOrder : Serializable
    {
        [JsonProperty("order_id", Required = Required.Always)]
        public string OrderId { get; set; }


        [JsonProperty("order_type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType OrderType { get; set; }


        [JsonProperty("priority", Required = Required.Always)]
        public int Priority { get; set; }

        [JsonProperty("ignore_stock_check", Required = Required.Default)]
        public bool IgnoreStockCheck { get; set; } = false;

        [JsonProperty("pallets", Required = Required.Always)]
        public List<AutomationOrderPallet> Pallets { get; set; } = new List<AutomationOrderPallet>();


        [JsonProperty("unpacked_articles", Required = Required.Always)]
        public List<AutomationOrderArticle> UnpackedArticles { get; set; } = new List<AutomationOrderArticle>();

        [JsonProperty("is_stock_removal_order", Required = Required.Default)]
        public bool IsStockRemovalOrder { get; set; } = false;

        public bool IsManualPackingRequired()
        {
            return UnpackedArticles.Count() > 0;
        }

        public bool IsValid()
        {
            var onlyNumericOrderId = bool.Parse(Environment.GetEnvironmentVariable("NUMERIC_ORDER_ID") ?? "false");

            if (string.IsNullOrEmpty(OrderId)) { return false; }
            if (onlyNumericOrderId && !long.TryParse(OrderId, out _)) { return false; }

            if (!Enum.IsDefined(typeof(OrderType), OrderType)) { return false; }

            if (Pallets.Count() < 1)
            {
                Console.WriteLine("AutomationOrder :: Order has no pallets");
                return false;
            }

            foreach(AutomationOrderPallet pallet in Pallets)
            {
                if (!pallet.IsValid()) { return false; }
            }
            return true;
        }

        public Dictionary<string, int> GetRequiredStock()
        {
            var requiredStock = new Dictionary<string, int>();
            foreach(AutomationOrderPallet pallet in Pallets)
            {
                foreach(AutomationOrderPackage package in pallet.Packages)
                {
                    if(requiredStock.ContainsKey(package.ArticleIdentifier))
                        requiredStock[package.ArticleIdentifier] += 1;
                    else
                        requiredStock[package.ArticleIdentifier] = 1;
                }
            }
            return requiredStock;
        }

        public ManualPackingOrder ToManualPackingOrder()
        {
            return new ManualPackingOrder()
            {
                OrderId = OrderId,
                Articles = UnpackedArticles
                    .Where(a => a.Amount > 0)
                    .Select(a => new ManualPackingArticle()
                    {
                        ArticleId = a.ArticleIdentifier,
                        Description = a.Description,
                        Amount = a.Amount
                    })
                    .ToList()
            };
        }
    }

    public class AutomationOrderArticle
    {
        [JsonProperty("article_identifier", Required = Required.Always)]
        public string ArticleIdentifier { get; set; }

        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; }

        [JsonProperty("width", Required = Required.Always)]
        public int Width { get; set; }

        [JsonProperty("height", Required = Required.Always)]
        public int Height { get; set; }

        [JsonProperty("length", Required = Required.Always)]
        public int Length { get; set; }

        [JsonProperty("weight", Required = Required.Always)]
        public double Weight { get; set; }

        [JsonProperty("amount", Required = Required.Always)]
        public int Amount { get; set; }
    }

    public class AutomationOrderPallet
    {
        [JsonProperty("pallet_index", Required = Required.Always)]
        public int PalletIndex { get; set; }

        [JsonProperty("packing_strategy", Required = Required.Always)]
        public PalletPackingStrategy PackingStrategy { get; set; }

        [JsonProperty("packages", Required = Required.Always)]
        public List<AutomationOrderPackage> Packages { get; set; }

        public bool IsValid()
        {
            if(PalletIndex < 0)
            {
                Console.WriteLine("AutomationOrderPallet :: PalletIndex is negative");
                return false;
            }
            if(Packages.Count() < 1)
            {
                Console.WriteLine("AutomationOrderPallet :: Pallet has no packages");
                return false;
            }
            foreach(AutomationOrderPackage package in Packages)
            {
                if(!package.IsValid()) { return false; }
            }
            return true;
        }

        //system: X = width, Y = height, Z = length
        public int FillHeight => Packages.Max(p => p.CenterY + p.Height / 2);
        
        public int FillWidth => Packages.Max(p => p.CenterX + p.Width / 2) - Packages.Min(p => p.CenterX - p.Width / 2);

        public int FillLength => Packages.Max(p => p.CenterZ + p.Length / 2) - Packages.Min(p => p.CenterZ - p.Length / 2);

        public int Weight => (int)Packages.Sum(p => p.Weight);
    }

    public class AutomationOrderPackage
    {
        [JsonProperty("article_identifier", Required = Required.Always)]
        public string ArticleIdentifier { get; set; }

        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; }

        [JsonProperty("index", Required = Required.Always)]
        public int Index { get; set; }


        [JsonProperty("width", Required = Required.Always)]
        public int Width { get; set; }

        [JsonProperty("height", Required = Required.Always)]
        public int Height { get; set; }

        [JsonProperty("length", Required = Required.Always)]
        public int Length { get; set; }

        [JsonProperty("weight", Required = Required.Always)]
        public double Weight { get; set; }

        //system: X = width, Y = height, Z = length
        [JsonProperty("center_x", Required = Required.Always)]
        public int CenterX { get; set; }

        [JsonProperty("center_y", Required = Required.Always)]
        public int CenterY { get; set; }

        [JsonProperty("center_z", Required = Required.Always)]
        public int CenterZ { get; set; }

        public bool IsValid()
        {
            if(CenterX < 0 || CenterY < 0 || CenterZ < 0)
            {
                Console.WriteLine("AutomationOrderPackage :: Center is negative");
                return false;
            }
            return true;
        }
    }
}

