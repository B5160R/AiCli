using Newtonsoft.Json;

namespace AiCli.Models.AiStudio;

public class Part
{
    [JsonProperty(PropertyName = "text")]
    public required string Text { get; set; }
}
