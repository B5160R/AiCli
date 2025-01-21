using Newtonsoft.Json;

namespace AiCli.Models.MistralAi;

public class MistralResponse
{
    [JsonProperty(PropertyName = "id")]
    public string? Id { get; set; }

    [JsonProperty(PropertyName = "object")]
    public string? Object { get; set; }

    [JsonProperty(PropertyName = "created")]
    public string? Created { get; set; }

    [JsonProperty(PropertyName = "model")]
    public string? Model { get; set; }

    [JsonProperty(PropertyName = "choices")]
    public IEnumerable<Choice>? Choices { get; set; }

    [JsonProperty(PropertyName = "usage")]
    public Usage? Usage { get; set; }
}
