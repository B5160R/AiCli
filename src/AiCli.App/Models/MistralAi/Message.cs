using Newtonsoft.Json;

namespace AiCli.Models.MistralAi;

public class Message
{
    [JsonProperty(PropertyName = "role")]
    public string? Role { get; set; }

    //public IEnumerable<string>? Tool_calls { get; set; }

    [JsonProperty(PropertyName = "content")]
    public string? Content { get; set; }
}
