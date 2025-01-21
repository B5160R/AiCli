using Newtonsoft.Json;

namespace AiCli.Models.AiStudio;

public class AiStudioResponse
{
    [JsonProperty(PropertyName = "candidates")]
    public required IEnumerable<Candidate> Candidates { get; set; }
}
