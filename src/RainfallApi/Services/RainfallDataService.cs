using Microsoft.Extensions.Options;
using RainfallApi.DTO;
using RainfallApi.Settings;

namespace RainfallApi.Services;

public class RainfallDataService
{
    private readonly HttpClient _httpClient;
    private readonly string _rainfallApiUrl;

    public RainfallDataService(HttpClient httpClient, IOptions<RainfallDataSettings> settings)
    {
        _httpClient = httpClient;
        _rainfallApiUrl = settings.Value.RainfallApiUrl;
    }

    public Task<RainfallReadingResponseDto> GetRainfallDataAsync(string stationId, int limit)
    {
        return Task.FromResult(new RainfallReadingResponseDto());
    }
}
