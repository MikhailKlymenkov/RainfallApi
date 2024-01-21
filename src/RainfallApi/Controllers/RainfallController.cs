using Microsoft.AspNetCore.Mvc;
using RainfallApi.DTO;
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
        try
        {
            RainfallReadingResponseDto apiResponse = await _rainfallDataService.GetRainfallDataAsync(stationId, count);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}