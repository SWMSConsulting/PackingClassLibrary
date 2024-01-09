using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PackingClassLibrary
{
    public enum OrderStatus
    {
        // dont change the order
        Created,
        Calculated, 
        ReadyToTransfer,

        Transferred,
        Error,
        NoMaterial,
        InProgress,
        Finished
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