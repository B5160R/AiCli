using Newtonsoft.Json;

namespace AiCli.Models.MistralAi;

public class Choice
{
    [JsonProperty(PropertyName = "index")]
    public int Index { get; set; }

    [JsonProperty(PropertyName = "message")]
    public Message? Message { get; set; }

    [JsonProperty(PropertyName = "finish_reason")]
    public string? Finish_reason { get; set; }
}
