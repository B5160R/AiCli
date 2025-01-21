using AiCli.Models.AiStudio;

namespace AiCli.Interfaces;

public interface IAiStudioApiCaller
{
    public Task<AiStudioResponse> GetRequest(AiStudioRequest request);
}
