using Microsoft.Extensions.Options;
using RainfallApi.DTO;
using RainfallApi.Settings;
using System.Text.Json;

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

    public async Task<RainfallReadingResponseDto> GetRainfallDataAsync(string stationId, int limit)
    {
        var url = string.Format(_rainfallApiUrl, stationId, limit);
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<RainfallReadingResponseDto>(content);
        return result == null ? throw new JsonException("Unsupported JSON format") : result;
    }
}
