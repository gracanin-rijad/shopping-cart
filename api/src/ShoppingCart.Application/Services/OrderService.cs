using ShoppingCart.Application.Repositories;
using ShoppingCart.Application.Requests;
using ShoppingCart.Domain;

namespace ShoppingCart.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository orderRepository;
    private readonly ICartRepository cartRepository;
    private readonly IProductService productService;

    public OrderService(
        IOrderRepository orderRepository,
        ICartRepository cartRepository,
        IProductService productService)
    {
        this.orderRepository = orderRepository;
        this.cartRepository = cartRepository;
        this.productService = productService;
    }

    public async Task<OrderDto?> CreateOrderAsync(CreateOrderRequest request)
    {
        var cartItems = await cartRepository.GetItemsAsync();
        if (!cartItems.Any())
            return null;

        var orderItems = new List<OrderItem>();
        var totalPrice = 0m;

        foreach (var cartItem in cartItems)
        {
            var product = await productService.GetProductByIdAsync(cartItem.ProductId);
            if (product is null)
                continue;

            var itemTotal = product.Price * cartItem.Quantity;
            totalPrice += itemTotal;

            orderItems.Add(new OrderItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = cartItem.Quantity,
                TotalPrice = itemTotal
            });
        }

        var order = new Order
        {
            CustomerName = request.CustomerName,
            CustomerEmail = request.CustomerEmail,
            ShippingAddress = request.ShippingAddress,
            City = request.City,
            Notes = request.Notes,
            TotalPrice = totalPrice,
            Items = orderItems
        };

        var savedOrder = await orderRepository.AddAsync(order);
        await cartRepository.ClearAsync();
        return MapToDto(savedOrder);
    }

    public async Task<OrderDto?> GetOrderByIdAsync(int orderId)
    {
        var order = await orderRepository.GetByIdAsync(orderId);
        return order is null ? null : MapToDto(order);
    }

    public async Task<List<OrderDto>> GetOrdersAsync()
    {
        var orders = await orderRepository.GetAllAsync();
        return orders.Select(MapToDto).ToList();
    }

    private static OrderDto MapToDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            CustomerName = order.CustomerName,
            CustomerEmail = order.CustomerEmail,
            ShippingAddress = order.ShippingAddress,
            City = order.City,
            Notes = order.Notes,
            TotalPrice = order.TotalPrice,
            CreatedAt = order.CreatedAt,
            Items = order.Items.Select(item => new OrderItemDto
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Price = item.Price,
                Quantity = item.Quantity,
                TotalPrice = item.TotalPrice
            }).ToList()
        };
    }
}
