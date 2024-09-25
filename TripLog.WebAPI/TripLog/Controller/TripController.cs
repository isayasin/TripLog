using Microsoft.AspNetCore.Mvc;
using TripLog.WebAPI.Models.DTOs;
using TripLog.WebAPI.Services;

namespace TripLog.WebAPI.Controller;
[Route("api/[controller]/[action]")]
[ApiController]
public class TripController(TripService tripService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] TripDTO tripDTO, CancellationToken cancellationToken)
    {
        await tripService.CreateTripAsync(tripDTO, cancellationToken);
        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        return Ok(await tripService.GetAllTripsAsync(cancellationToken));
    }

}
