using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application;

namespace ShoppingCart.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        var products = await productService.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await productService.GetProductByIdAsync(id);
        if (product is null)
            return NotFound();

        return Ok(product);
    }
}
