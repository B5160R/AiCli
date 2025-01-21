namespace AiCli.Interfaces;

public interface IRateLimiter
{
    public Task AquireSlot();
    public Task ReleaseSlot();
}
