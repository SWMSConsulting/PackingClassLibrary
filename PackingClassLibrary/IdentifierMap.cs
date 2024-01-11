using Newtonsoft.Json;

namespace PackingClassLibrary
{
    public class IdentifierMap : Serializable
    {
        [JsonProperty("article_id", Required = Required.AllowNull)]
        public string ArticleId { get; set; }


        [JsonProperty("grundner_id", Required = Required.AllowNull)]
        public int? GrundnerId { get; set; }

        [JsonProperty("message", Required = Required.Default)]
        public string? Message { get; set; } = null;
    }
}
