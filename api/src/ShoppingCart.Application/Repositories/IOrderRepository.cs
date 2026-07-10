using ShoppingCart.Domain;

namespace ShoppingCart.Application.Repositories;

public interface IOrderRepository
{
    Task<Order> AddAsync(Order order);
    Task<Order?> GetByIdAsync(int orderId);
    Task<List<Order>> GetAllAsync();
}
