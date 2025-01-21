using Newtonsoft.Json;

namespace AiCli.Models.AiStudio;

public class Content
{
    [JsonProperty(PropertyName = "parts")]
    public required IEnumerable<Part> Parts { get; set; }
}
