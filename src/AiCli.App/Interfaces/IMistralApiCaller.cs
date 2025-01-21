using AiCli.Models.MistralAi;

namespace AiCli.Interfaces;

public interface IMistralApiCaller
{
    public Task<MistralResponse> GetRequest(MistralRequest request);
}
