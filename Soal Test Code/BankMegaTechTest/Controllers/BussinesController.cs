using BankMegaTechTest.Data;
using Microsoft.AspNetCore.Mvc;
using BankMegaTechTest.Services.Contract;
using BankMegaTechTest.Models.DTO;

namespace BankMegaTechTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BussinessController : ControllerBase
{

    private readonly ILogger<BussinessController> _logger;

    private readonly ApplicationDbContext _context;

    private readonly IBusinessService _businessService;

    public BussinessController(ILogger<BussinessController> logger, ApplicationDbContext context, IBusinessService businessService)
    {
        _logger = logger;
        _context = context;
        _businessService = businessService;
    }

    [HttpPost("insert")]
    public async Task<IActionResult> InsertBpkb([FromBody] InsertBpkbRequestDTO requestDto)
    {
        try
        {
            var response = await _businessService.InsertBpkb(requestDto);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateBpkb([FromBody] InsertBpkbRequestDTO requestDto)
    {
        try
        {
            var response = await _businessService.UpdateBpkb(requestDto);
            if (response.Success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("bpkb")]
    public async Task<IActionResult> GetBpkbsByUserId([FromQuery] long userId)
    {
        try
        {
            var bpkbs = await _businessService.GetBpkbByUserIdAsync(userId);
            if (bpkbs == null)
            {
                return NotFound(new { Message = "No BPKB records found for this user." });
            }
            return Ok(bpkbs);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("location/all")]
    public async Task<IActionResult> GetAllLocations()
    {
        try
        {
            var locations = await _businessService.GetAllLocation();
            return Ok(locations);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet("test-connection")]
    public async Task<IActionResult> TestConnection()
    {
        try
        {
            var canConnect = await _context.Database.CanConnectAsync();
            if (canConnect)
            {
                return Ok("Connection successful.");
            }
            else
            {
                return StatusCode(500, "Connection failed.");
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Exception occurred: {ex.Message}");
        }
    }
}

