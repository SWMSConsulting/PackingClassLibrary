using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PackingClassLibrary
{
    public enum AutomationStatusEnum
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

    public class AutomationStatus
    {
        [JsonProperty("order_id")]
        public string OrderId { get; set; }


        [JsonProperty("status", Required = Required.AllowNull)]
        [JsonConverter(typeof(StringEnumConverter))]
        public AutomationStatusEnum Status { get; set; }


        [JsonProperty("message")]
        public string Message { get; set; }

        public AutomationStatus(
            string orderId, 
            AutomationStatusEnum status, 
            string message = ""
        ) {
            OrderId = orderId;
            Status = status;
            Message = message;
        }
    }
}