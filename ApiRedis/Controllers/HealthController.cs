using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace ApiRedis.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController(IDistributedCache cache) : ControllerBase
{
    [HttpGet("redis")]
    public async Task<IActionResult> CheckRedis()
    {
        try
        {
            var testKey = "health-check";
            var testValue = $"ok-{DateTime.UtcNow:O}";

            // Write
            await cache.SetStringAsync(testKey, testValue, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
            });

            // Read
            var result = await cache.GetStringAsync(testKey);

            // Delete
            await cache.RemoveAsync(testKey);

            if (result == testValue)
            {
                return Ok(new
                {
                    status = "healthy",
                    message = "Redis write/read/delete OK",
                    timestamp = DateTime.UtcNow
                });
            }

            return StatusCode(500, new
            {
                status = "unhealthy",
                message = $"Read mismatch: expected '{testValue}', got '{result}'"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                status = "unhealthy",
                message = ex.Message
            });
        }
    }
}
