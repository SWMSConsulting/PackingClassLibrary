using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PackingClassLibrary
{
    public enum CrudTask
    {
        None,
        Create,
        Read,
        Update,
        Delete
    }

    public class IdentifierMap : Serializable
    {
        [JsonProperty("task", Required = Required.Always)]
        public CrudTask Task { get; set; }

        [JsonProperty("article_id", Required = Required.AllowNull)]
        public string? ArticleId { get; set; }

        [JsonProperty("grundner_id", Required = Required.AllowNull)]
        public int? GrundnerId { get; set; }

        [JsonProperty("message", Required = Required.Default)]
        public string? Message { get; set; }
    }
}
