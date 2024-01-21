using Microsoft.AspNetCore.Mvc;
using RainfallApi.DTO;
using RainfallApi.Model;
using RainfallApi.Model.Errors;
using RainfallApi.Services;

namespace RainfallApi.Controllers;

[ApiController]
[Route("rainfall")]
public class RainfallController : ControllerBase
{
    private readonly RainfallDataService _rainfallDataService;

    public RainfallController(RainfallDataService rainfallDataService)
    {
        _rainfallDataService = rainfallDataService;
    }

    [HttpGet("id/{stationId}/readings")]
    public async Task<IActionResult> GetReadingsAsync(string stationId, [FromQuery] int count = 10)
    {
        if (string.IsNullOrWhiteSpace(stationId))
        {
            return BadRequest(CreateValidationError(nameof(stationId), "stationId cannot be null or empty"));
        }

        if (count < 1 || count > 100)
        {
            return BadRequest(CreateValidationError(nameof(count), "count must be from 1 to 100"));
        }

        try
        {
            RainfallReadingResponseDto apiResponse = await _rainfallDataService.GetRainfallDataAsync(stationId, count);
            if (apiResponse.Items == null || !apiResponse.Items.Any())
            {
                return NotFound(new Error("No readings found for the specified stationId"));
            }

            return Ok(MapDtoToModel(apiResponse));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Error(ex.Message));
        }
    }

    private RainfallReadingResponse MapDtoToModel(RainfallReadingResponseDto apiResponse)
    {
        var response = new RainfallReadingResponse
        {
            Readings = new List<RainfallReading>()
        };

        foreach (var readingDto in apiResponse.Items!)
        {
            var reading = new RainfallReading
            {
                DateMeasured = readingDto.DateTime,
                AmountMeasured = readingDto.Value
            };

            response.Readings.Add(reading);
        }

        return response;
    }

    private Error CreateValidationError(string propertyName, string message)
    {
        return new Error("Invalid request")
        {
            Detail = new List<ErrorDetail>()
            {
                new ()
                {
                    PropertyName = propertyName,
                    Message = message
                }
            }
        };
    }
}