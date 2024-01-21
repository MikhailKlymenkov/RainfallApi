using System.Text.Json.Serialization;

namespace RainfallApi.DTO;

public class RainfallReadingResponseDto
{
    [JsonPropertyName("items")]
    public List<RainfallReadingDto>? Items { get; set; }
}
