
using Microsoft.AspNetCore.Mvc;
using SimpleNameApi.DTOs.Requests;
using SimpleNameApi.DTOs.Responses;
using SimpleNameApi.Interfaces;

namespace SimpleNameApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NamesController : ControllerBase
{
    private readonly INameService _nameService;

    public NamesController(INameService nameService)
    {
        _nameService = nameService;
    }

    [HttpPost]
    public ActionResult<NameResponse> CreateName(
        [FromBody] CreateNameRequest request)
    {
        var result = _nameService.CreateName(request);

        return StatusCode(
            StatusCodes.Status201Created,
            result
        );
    }

    [HttpGet]
    public ActionResult<List<NameResponse>> GetAllNames()
    {
        var names = _nameService.GetAllNames();

        return Ok(names);
    }
}
