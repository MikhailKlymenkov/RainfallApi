using System.Text.Json.Serialization;

namespace RainfallApi.DTO;

public class RainfallReadingDto
{
    [JsonPropertyName("@id")]
    public required string Id { get; set; }

    [JsonPropertyName("dateTime")]
    public DateTime DateTime { get; set; }

    [JsonPropertyName("measure")]
    public required string Measure { get; set; }

    [JsonPropertyName("value")]
    public decimal Value { get; set; }
}
