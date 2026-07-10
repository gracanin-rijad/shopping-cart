using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Application;
using ShoppingCart.Application.Requests;
using ShoppingCart.Application.Services;

namespace ShoppingCart.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var order = await orderService.CreateOrderAsync(request);
        if (order is null)
            return BadRequest(new { message = "Cart is empty or order could not be created." });

        return CreatedAtAction(nameof(GetOrderById), new { orderId = order.Id }, order);
    }

    [HttpGet]
    public async Task<ActionResult<List<OrderDto>>> GetOrders()
    {
        var orders = await orderService.GetOrdersAsync();
        return Ok(orders);
    }

    [HttpGet("{orderId}")]
    public async Task<ActionResult<OrderDto>> GetOrderById(int orderId)
    {
        var order = await orderService.GetOrderByIdAsync(orderId);
        if (order is null)
            return NotFound();

        return Ok(order);
    }
}
