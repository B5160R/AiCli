using System.Text;
using AiCli.Interfaces;
using AiCli.Models.AiStudio;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AiCli.Services;

public class AiStudioApiCaller(
    ILogger<AiStudioApiCaller> _logger,
    HttpClient _httpClient,
    IMemoryCache _cache,
    IRateLimiter _rateLimiter
) : IAiStudioApiCaller
{
    public async Task<AiStudioResponse> GetRequest(AiStudioRequest request)
    {
        var payload = JsonConvert.SerializeObject(request);

        if (_cache.TryGetValue(payload, out string? cachedResponse))
        {
            if (cachedResponse is not null)
            {
                return JsonConvert.DeserializeObject<AiStudioResponse>(cachedResponse)
                    ?? throw new ArgumentNullException("Cached response object was null");
            }
        }

        await _rateLimiter.AquireSlot();
        try
        {
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("", content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Error Response: " + errorContent);
            }

            response.EnsureSuccessStatusCode();

            var contentString = await response.Content.ReadAsStringAsync();

            var result =
                JsonConvert.DeserializeObject<AiStudioResponse>(contentString)
                ?? throw new ArgumentNullException("Response object was null");

            // Cache the response content for 5 minutes using the cache key
            _cache.Set(payload, contentString, TimeSpan.FromMinutes(5));

            return result;
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Error during request to API");
            throw;
        }
        finally
        {
            await _rateLimiter.ReleaseSlot();
        }
    }
}
