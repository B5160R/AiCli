using AiCli.Interfaces;

namespace AiCli.Services;

public class RateLimiter : IRateLimiter
{
    private const int _maxRequestPerSecond = 1;
    private readonly SemaphoreSlim _rateLimiter = new SemaphoreSlim(
        _maxRequestPerSecond,
        _maxRequestPerSecond
    );

    // Waits to acquire a slot in the rate limiter
    public async Task AquireSlot()
    {
        await _rateLimiter.WaitAsync();
    }

    // Releases the rate limiter after a delay to enforce the rate limit
    public async Task ReleaseSlot()
    {
        await Task.Delay(TimeSpan.FromSeconds(1.0 / _maxRequestPerSecond))
            .ContinueWith(t => _rateLimiter.Release());
    }
}
