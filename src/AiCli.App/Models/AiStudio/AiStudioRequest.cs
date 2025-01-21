using Newtonsoft.Json;

namespace AiCli.Models.AiStudio;

public class AiStudioRequest
{
    [JsonProperty(PropertyName = "contents")]
    public required IEnumerable<Content> Contents { get; set; }
}
