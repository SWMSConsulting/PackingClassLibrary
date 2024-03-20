using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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


        [JsonProperty("pallets", Required = Required.Always)]
        public List<AutomationOrderPallet> Pallets { get; set; } = new List<AutomationOrderPallet>();


        [JsonProperty("unpacked_articles", Required = Required.Always)]
        public List<AutomationOrderArticle> UnpackedArticles { get; set; } = new List<AutomationOrderArticle>();

        public bool IsManualPackingRequired()
        {
            return UnpackedArticles.Count() > 0;
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(OrderId)) { return false; }
            if (!long.TryParse(OrderId, out _)) { return false; }

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
    }

    public class AutomationOrderArticle
    {
        [JsonProperty("article_number", Required = Required.Always)]
        public int ArticleNumber { get; set; }

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

        [JsonProperty("pallet_identifier", Required = Required.Default)]
        public string? PalletIdentifier { get; set; } = null;

        [JsonProperty("protection_plate_identifier", Required = Required.Default)]
        public string? ProtectionPlateIdentifier { get; set; } = null;

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
    }

    public class AutomationOrderPackage
    {
        [JsonProperty("article_identifier", Required = Required.Always)]
        public string ArticleIdentifier { get; set; }


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


        [JsonProperty("center_x", Required = Required.Always)]
        public int CenterX { get; set; }

        [JsonProperty("center_y", Required = Required.Always)]
        public int CenterY { get; set; }

        [JsonProperty("center_z", Required = Required.Always)]
        public int CenterZ { get; set; }

        public bool IsValid()
        {
            return true;
        }
    }
}

