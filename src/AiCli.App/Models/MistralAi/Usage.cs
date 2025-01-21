using Newtonsoft.Json;

namespace AiCli.Models.MistralAi;

public class Usage
{
    [JsonProperty(PropertyName = "prompt_tokens")]
    public int Prompt_tokens { get; set; }

    [JsonProperty(PropertyName = "total_tokens")]
    public int Total_tokens { get; set; }

    [JsonProperty(PropertyName = "completeion_tokens")]
    public int Completion_tokens { get; set; }
}
