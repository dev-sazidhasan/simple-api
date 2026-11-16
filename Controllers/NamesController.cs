
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

    [HttpGet("{id:guid}")]
    public ActionResult<NameResponse> GetNameById(Guid id)
    {
        var name = _nameService.GetNameById(id);

        if (name is null)
        {
            return NotFound(new
            {
                message = "Name not found."
            });
        }

        return Ok(name);
    }

    [HttpPut("{id:guid}")]
    public ActionResult<NameResponse> UpdateName(
        Guid id,
        [FromBody] UpdateNameRequest request)
    {
        var updatedName = _nameService.UpdateName(id, request);

        if (updatedName is null)
        {
            return NotFound(new
            {
                message = "Name not found."
            });
        }

        return Ok(updatedName);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteName(Guid id)
    {
        bool deleted = _nameService.DeleteName(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Name not found."
            });
        }

        return NoContent();
    }
}
