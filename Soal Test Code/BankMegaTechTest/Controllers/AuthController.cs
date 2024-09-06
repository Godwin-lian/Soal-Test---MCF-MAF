using BankMegaTechTest.Models.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using BankMegaTechTest.Services.Contract;
using BankMegaTechTest.Services;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BankMegaTechTest.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authenticationService;

    public AuthController(
        IAuthService authenticationService
    )
    {
        _authenticationService = authenticationService;
    }

    private class Login { };

    [HttpPost("login", Name = nameof(Login))]
    public async Task<ActionResult<LoginResponseDTO>> Authenticate([FromBody] LoginRequestDTO request)
    {
        try
        {
            var authenticationResponse = await _authenticationService.AuthenticateByUsernamePassword(request);
            return Ok(authenticationResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Controller: An error occurred during authentication. {ex.Message}");
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequest)
    {
        if (registerRequest == null)
        {
            return BadRequest("Invalid registration request");
        }

        var result = await _authenticationService.Register(registerRequest);

        if (result.Success)
        {
            return Ok(result);
        }
        else
        {
            return Conflict("User already exists");
        }
    }

}

