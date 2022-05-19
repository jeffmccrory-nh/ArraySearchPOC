using System.Text.Json.Serialization;

namespace ArraySearchPOC.Models
{
    public class Annotation
    {
        [JsonPropertyName("page_id")]
        public int PageId { get; set; } = 0;

        [JsonPropertyName("box")]
        public int[]? Box { get; set; }
    }

}
