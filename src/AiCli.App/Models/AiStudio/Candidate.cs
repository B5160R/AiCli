using Newtonsoft.Json;

namespace AiCli.Models.AiStudio;

public class Candidate
{
    [JsonProperty(PropertyName = "content")]
    public required Content Content { get; set; }
}
