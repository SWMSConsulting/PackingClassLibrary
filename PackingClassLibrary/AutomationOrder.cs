using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PackingClassLibrary
{
    public class AutomationOrder : Serializable
    {
        [JsonProperty("order_id", Required = Required.Always)]
        public string OrderId { get; set; }


        [JsonProperty("order_type", Required = Required.Always)]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType OrderType { get; set; }


        [JsonProperty("pallets", Required = Required.Always)]
        public List<AutomationOrderPallet> Pallets { get; set; } = new List<AutomationOrderPallet>();


        [JsonProperty("unpacked_articles", Required = Required.Always)]
        public List<AutomationOrderArticle> UnpackedArticles { get; set; } = new List<AutomationOrderArticle>();

        public bool IsValid()
        {
            if(!long.TryParse(OrderId, out _))
            {
                Console.WriteLine("AutomationOrder :: OrderId is not numeric");
                return false;
            }

            if(Pallets.Count() < 1)
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
                    if(requiredStock.ContainsKey(package.ArticleId))
                    {
                        requiredStock[package.ArticleId] += 1;
                    } else
                    {
                        requiredStock[package.ArticleId] = 1;
                    }
                }
            }
            return requiredStock;
        }
    }

    public class AutomationOrderArticle
    {
        [JsonProperty("article_id", Required = Required.Always)]
        public string ArticleId { get; set; }

        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; }


        [JsonProperty("width", Required = Required.Always)]
        public int Width { get; set; }

        [JsonProperty("height", Required = Required.Always)]
        public int Height { get; set; }

        [JsonProperty("length", Required = Required.Always)]
        public int Length { get; set; }


        [JsonProperty("amount", Required = Required.Always)]
        public int Amount { get; set; }
    }

    public class AutomationOrderPallet
    {
        [JsonProperty("pallet_index", Required = Required.Always)]
        public int PalletIndex { get; set; }

        [JsonProperty("width", Required = Required.Always)]
        public int Width { get; set; }

        [JsonProperty("length", Required = Required.Always)]
        public int Length { get; set; }

        [JsonProperty("packages", Required = Required.Always)]
        public List<AutomationOrderPackage> Packages { get; set; }

        public bool IsValid()
        {
            if(PalletIndex < 0)
            {
                Console.WriteLine("AutomationOrderPallet :: PalletIndex is negative ");
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
        [JsonProperty("article_id", Required = Required.Always)]
        public string ArticleId { get; set; }

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

