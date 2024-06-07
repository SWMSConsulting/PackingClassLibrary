
namespace PackingClassLibrary
{
    [Serializable]
    public class OrderExport
    {
        public string? OrderNumber { get; set; }
        public string? CustomerName { get; set; }
        public byte[]? Pdf { get; set; }
        public Guid Oid { get; set; }
        public byte[]? PdfImage { get; set; }
        public ArticleExport[]? Articles { get; set; }
        public List<PackageImport>? Packages { get; set; } = new List<PackageImport>();
        public OrderStatusExportEnum? OrderStatus { get; set; }
        public string? AdressName { get; set; }
        public string? AdressNumber { get; set; }
        public string? AdressStreet { get; set; }
        public string? AdressPLZ { get; set; }
        public string? AdressPlace { get; set; }
        public string? Employee { get; set; }
        public DateTime? CompletionDateTarget { get; set; }
        public Guid[] CombinedPrint { get; set; } = new Guid[0];
        public string OrderType { get; set; }
    }

    [Serializable]
    public enum OrderStatusExportEnum
    {
        Received = 0,
        Confirmed = 2,
        In_Production = 4,
        Picked = 5,
        Shipped = 6,
        finished = 9,
        Cancelled = 7,
        OnHold = 8,
        MaterialMissing = 10
        
    }

    [Serializable]
    public class ArticleExport
    {
        public double? Amount { get; set; }
        public string? ItemNo { get; set; } = "";
        public string? Description { get; set; } = "";
        public string? Matchcode { get; set; } = "";
        public bool? picked { get; set; } = false;
        public Guid Oid { get; set; }
    }

    [Serializable]
    public class PackageImport
    {
        public double? Height { get; set; }
        public double? Width { get; set; }
        public double? Length { get; set; }
        public double? Weight { get; set; } = null;
        public string? WeightReference { get; set; }
        public string? SSCC { get; set; }
        public Guid? oId { get; set; }
    }
}
