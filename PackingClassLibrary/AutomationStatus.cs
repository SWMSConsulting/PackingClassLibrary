using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PackingClassLibrary
{
    public enum OrderStatus
    {
        // dont change the order
        Created = 0,
        Calculated = 1, 

        ReadyToTransfer = 2,
        Transferred = 3,

        Blocked = 5,
        NoMaterial = 6,

        InProgress = 8,
        ManualPacking = 9,
   
        Error = 10,
        Finished = 12,


    }

    public class AutomationStatus : Serializable
    {
        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("status", Required = Required.AllowNull)]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderStatus Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("pallet_index")]
        public int? PalletIndex { get; set; }

        public AutomationStatus(
            string orderId, 
            OrderStatus status, 
            string message = ""
        ) {
            OrderId = orderId;
            Status = status;
            Message = message;
        }
    }
}