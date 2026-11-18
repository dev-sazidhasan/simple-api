using Microsoft.AspNetCore.Mvc;
using SimpleNameApi.DTOs.Requests;
using SimpleNameApi.DTOs.Responses;
using SimpleNameApi.Interfaces;

namespace SimpleNameApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public ActionResult<AuthResponse> Register(
        [FromBody] RegisterRequest request)
    {
        var response = _authService.Register(request);

        return StatusCode(
            StatusCodes.Status201Created,
            response
        );
    }

    [HttpPost("login")]
    public ActionResult<AuthResponse> Login(
        [FromBody] LoginRequest request)
    {
        var response = _authService.Login(request);

        return Ok(response);
    }
}