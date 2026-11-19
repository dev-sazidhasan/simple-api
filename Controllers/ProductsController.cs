using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SimpleNameApi.DTOs.Requests;
using SimpleNameApi.DTOs.Responses;
using SimpleNameApi.Interfaces;

namespace SimpleNameApi.Controllers;

[ApiController]
[Route("api/products")]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public ActionResult<ProductResponse> Create(
        [FromBody] CreateProductRequest request)
    {
        var response = _productService.Create(request);

        return StatusCode(
            StatusCodes.Status201Created,
            response
        );
    }

    [HttpGet]
    public ActionResult<List<ProductResponse>> GetAll()
    {
        var products = _productService.GetAll();

        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    public ActionResult<ProductResponse> GetById(Guid id)
    {
        var product = _productService.GetById(id);

        if (product is null)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        return Ok(product);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public ActionResult<ProductResponse> Update(
        Guid id,
        [FromBody] UpdateProductRequest request)
    {
        var product = _productService.Update(id, request);

        if (product is null)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        return Ok(product);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public IActionResult Delete(Guid id)
    {
        var deleted = _productService.Delete(id);

        if (!deleted)
        {
            return NotFound(new
            {
                message = "Product not found."
            });
        }

        return Ok(new
        {
            message = "Product deleted successfully."
        });
    }
}