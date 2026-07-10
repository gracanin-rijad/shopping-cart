using ShoppingCart.Application.Requests;

namespace ShoppingCart.Application.Services;

public interface IOrderService
{
    Task<OrderDto?> CreateOrderAsync(CreateOrderRequest request);
    Task<OrderDto?> GetOrderByIdAsync(int orderId);
    Task<List<OrderDto>> GetOrdersAsync();
}
