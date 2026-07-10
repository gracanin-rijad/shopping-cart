using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application;
using ShoppingCart.Application.Requests;
using ShoppingCart.Application.Services;

namespace ShoppingCart.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartController(ICartService cartService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<CartDto>> GetCart()
    {
        var cart = await cartService.GetCartAsync();
        return Ok(cart);
    }

    [HttpPost("items")]
    public async Task<ActionResult<CartDto>> AddItem([FromBody] AddCartItemRequest request)
    {
        var cart = await cartService.AddItemAsync(request);
        if (cart is null)
            return NotFound(new { message = "Product not found." });

        return CreatedAtAction(nameof(GetCart), cart);
    }

    [HttpPut("items/{itemId}")]
    public async Task<ActionResult<CartDto>> UpdateItem(int itemId, [FromBody] UpdateCartItemRequest request)
    {
        var cart = await cartService.UpdateItemAsync(itemId, request);
        if (cart is null)
            return NotFound(new { message = "Cart item not found." });

        return Ok(cart);
    }

    [HttpDelete("items/{itemId}")]
    public async Task<ActionResult<CartDto>> RemoveItem(int itemId)
    {
        var cart = await cartService.RemoveItemAsync(itemId);
        if (cart is null)
            return NotFound(new { message = "Cart item not found." });

        return Ok(cart);
    }
}
