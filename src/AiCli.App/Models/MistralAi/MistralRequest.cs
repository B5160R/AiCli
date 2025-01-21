using Newtonsoft.Json;

namespace AiCli.Models.MistralAi;

public class MistralRequest
{
    [JsonProperty(PropertyName = "model")]
    public string? Model { get; set; }

    [JsonProperty(PropertyName = "messages")]
    public IEnumerable<Message>? Messages { get; set; }
}
